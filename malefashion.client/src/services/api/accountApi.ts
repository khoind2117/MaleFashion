import apiClient from "./apiClient";
import { AxiosResponse } from "axios";
import { RegisterData, LoginData, AuthResponseDto } from "../../context/auth";

const accountApi = {                  
    register: (data: RegisterData): Promise<AxiosResponse<AuthResponseDto>> => {
        return apiClient.post("/account/register", data);
    },
    login: (data: LoginData): Promise<AxiosResponse<AuthResponseDto>> => {
        return apiClient.post("/account/login", data);
    },
    refreshAccessToken: (refreshToken: string): Promise<AxiosResponse<AuthResponseDto>> => {
        return apiClient.post("/account/refresh-token", { refreshToken });
    },
};

export default accountApi;
