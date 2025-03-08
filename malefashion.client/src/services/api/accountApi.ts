import apiClient from "./apiClient";
import { AxiosResponse } from "axios";
import { RegisterData, LoginData, AuthResponseDto } from "../../context/auth";
import { UserDto } from "../../models/dtos/User/UserDto";

const accountApi = {     
    getProfile: (): Promise<AxiosResponse<UserDto>> => {
        return apiClient.get("/account/me");
    },
    register: (data: RegisterData): Promise<AxiosResponse<AuthResponseDto>> => {
        return apiClient.post("/account/register", data);
    },
    login: (data: LoginData): Promise<AxiosResponse> => {
        return apiClient.post("/account/login", data, { withCredentials: true });
    },
    logout: (): Promise<AxiosResponse<void>> => {
        return apiClient.post("/account/logout");
    },
    refreshAccessToken: () : Promise<AxiosResponse<AuthResponseDto>> => {
        return apiClient.post("/account/refresh-token", {}, { withCredentials: true });
    },
};

export default accountApi;
