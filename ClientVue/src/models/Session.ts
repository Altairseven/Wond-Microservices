export interface Session {
    token: string;
    expiration: string;
    refreshToken: string;
    refreshExpiration: string;
}

export interface User {
    userId: number;
    userName: string;
}

export interface LoginResponse {
    userId: number;
    userName: string;
    token: string;
    expiration: string;
    refreshToken: string;
    refreshExpiration: string;
}

export function MapSession(res: LoginResponse) : [Session, User] {
    const session: Session = {
        token: res.token,
        expiration: res.expiration,
        refreshToken: res.refreshToken,
        refreshExpiration: res.refreshExpiration
    }

    const User: User = {
        userId: res.userId,
        userName: res.userName
    }
    return [session, User];
}