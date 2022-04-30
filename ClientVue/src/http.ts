import axios from "axios";
import { MapSession, Session } from "./models/Session";

import { useAuthStore } from "./store/Index";

debugger;
// const httpPublic = axios.create({
//     baseURL: import.meta.env.VITE_AUTH_URL,
//     timeout: 300,
//     headers: { 'content-type': 'application/json' }
// });


const http = axios.create({
    baseURL: import.meta.env.VITE_AUTH_URL,
    timeout: 300,
    headers: {
        'content-type': 'application/json',
        // "Authorization": `Bearer ${localStorage.getItem("Token")}`


    }
});


function isExpired(expiration: string): boolean {
    var date = new Date(expiration);
    var now = new Date();
    return date > now;
}

http.interceptors.request.use(async req => {
    const session = localStorage.getItem('session') ? (JSON.parse(localStorage.getItem('session') ?? "") as Session) : null
    if (session?.token) {
        req.headers!.Authorization = `Bearer ${session?.token}`;
    }

    // if (!isExpired(session?.expiration!)) return req

    // const refreshResponse = await axios.post(`${import.meta.env.VITE_AUTH_URL}/api/refresh-token/`, {
    //     expiredToken: session?.token,
    //     refreshToken: session?.refreshToken
    // });

    // const newSession = MapSession(refreshResponse.data)[0];

    // localStorage.setItem('session', JSON.stringify(newSession))
    // req.headers!.Authorization = `Bearer ${newSession.token}`
    return req
});




http.interceptors.response.use(async req => req,
    async (error) => {
        const originalConfig = error.config;
        const session = localStorage.getItem('session') ? (JSON.parse(localStorage.getItem('session') ?? "") as Session) : null
        debugger;
        if (error?.response.status == 401 && !originalConfig._retry) {
            originalConfig._retry = true;

            try {
                const isTokenExpired = isExpired(session?.expiration!);
                const isRefreshTokenExpired = isExpired(session?.refreshExpiration!);

                if(isTokenExpired && isRefreshTokenExpired)
                    throw Error("SESSION_DEAD");


                const refreshResponse = await axios.post(`${import.meta.env.VITE_AUTH_URL}/refresh-token/`, {
                    expiredToken: session?.token,
                    refreshToken: session?.refreshToken
                });
                const newSession = MapSession(refreshResponse.data)[0];
                localStorage.setItem('session', JSON.stringify(newSession))
                originalConfig.headers!.Authorization = `Bearer ${newSession.token}`
                console.log("session renewed");
                return http(originalConfig);

            } catch (error) {
                useAuthStore().doLogout();
                return Promise.reject("SESSION_DEAD");
            }
        }
        //cualquier otro codigo de error.
        return Promise.reject(error.response.data);
    }
)


export { http };


