import { createContext, useState, ReactNode } from "react";
import { AuthResponseDto, LoginData } from "./auth";
import accountApi from "../services/api/accountApi";

interface AuthContextType {
    user: AuthResponseDto | null;
    login: (data: LoginData) => Promise<void>;
    logout: () => void;
}

export const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [user, setUser] = useState<AuthResponseDto | null>(null);

    const login = async (data: LoginData) => {
        try {
            const response = await accountApi.login(data);

            if (response.status === 200) {
                const userData: AuthResponseDto = response.data;
                setUser(userData);

                localStorage.setItem("accessToken", userData.token);
                localStorage.setItem("refreshToken", userData.refreshToken);
            } else {
                throw new Error("Đăng nhập thất bại");
            }
        } catch (error) {
            console.error("Login failed:", error);
        }
    };
    
    const logout = () => {
        setUser(null);
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
    };

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};
