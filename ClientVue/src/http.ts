import axios from "axios";
import { AUTH_URL } from "./API_URL";


const httpPublic = axios.create({
    baseURL: AUTH_URL,
    timeout: 300,
    headers: { 'content-type': 'application/json' }
});


const http = axios.create({
    baseURL: AUTH_URL,
    timeout: 300,
    headers: {
        'content-type': 'application/json',
        "Authorization": `Bearer ${localStorage.getItem("Token")}`

    }
});


export { http, httpPublic };


