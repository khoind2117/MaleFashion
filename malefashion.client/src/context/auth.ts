export interface RegisterData {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    address: string;
    phoneNumber:string; 
}

export interface LoginData {
    userName: string;
    password: string;
}

export interface AuthResponseDto {
    userId: string;
    userName: string;
    email: string;
    roles: string[];
    token: string;
    refreshToken: string;
}
