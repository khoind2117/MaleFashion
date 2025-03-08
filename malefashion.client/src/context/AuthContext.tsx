import { createContext, useState, ReactNode, useEffect } from "react";
import { LoginData, RegisterData, UserData } from "./auth";
import accountApi from "../services/api/accountApi";
import { useNavigate } from "react-router";
import React from "react";
import { toast } from "react-toastify";
import axios from "axios";
import { useCookies } from "react-cookie";
import { useDispatch } from "react-redux";
import { resetCart } from "../store/cartSlice";

interface AuthContextType {
    user: UserData | null;
    token: string | null;
    register: (data: RegisterData) => Promise<void>;
    login: (data: LoginData) => Promise<void>;
    logout: () => void;
    isAuthenticated: () => boolean;
}

export const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [ , , removeCookie] = useCookies(['BasketId', 'refreshToken']);
    const [user, setUser] = useState<UserData | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [isReady, setIsReady] = useState(false);

    useEffect(() => {
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("accessToken");

        if (user && token) {
            setUser(JSON.parse(user));
            setToken(token);
        }

        setIsReady(true);
    }, []);

    const register = async (data: RegisterData) => {
        try {
            const response = await accountApi.register(data);
            if (response.status === 200) {
                toast.success("Đăng ký thành công, chuyển hướng đến trang đăng nhập.");
                navigate("/login");
            } else {
                toast.error("Đăng ký thất bại");
                throw new Error("Đăng ký thất bại");
            }
        } catch (error) {
            toast.error("Đăng ký thất bại");
            console.error("Register failed:", error);
        }
    };

    const login = async (data: LoginData) => {
        try {
            const response = await accountApi.login(data);
            if (response.status === 200) {
                // AuthResponseDto
                const { accessToken, ...userData } = response.data;

                setUser(userData);
                setToken(accessToken);

                localStorage.setItem("user", JSON.stringify({
                    userId: userData.userId,
                    userName: userData.userName,
                    email: userData.email,
                    roles: userData.roles
                }));
                localStorage.setItem("accessToken", accessToken);
                                
                navigate("/cart");
            }
        }
        catch (error) {
            if (axios.isAxiosError(error)) {    
                if (error.response) {
                    console.error("Login failed:", error.response.data);
                    toast.error(error.response.data.message || "Login failed. Please check your credentials.");
                } else {
                    console.error("No response from server:", error.message);
                    toast.error("Server is not responding. Please try again later.");
                }
            } else {
                console.error("Unexpected error:", error);
                toast.error("An unexpected error occurred.");
            }
        }
    };
    
    const logout = async () => {
        try {
            await accountApi.logout();

            setUser(null);
            setToken(null);
            localStorage.removeItem("user");
            localStorage.removeItem("accessToken");

            dispatch(resetCart());

            removeCookie('BasketId', { path: '/' });
            removeCookie('refreshToken', { path: '/' });
        } catch (error) {
            console.error("Logout failed", error);
        }

        navigate("/");
    };

    const isAuthenticated = () => {
        return !!user && !!token;
    };

      
    return (
        <AuthContext.Provider value={{ user, token, register, login, logout, isAuthenticated }}>
            {isReady ? children : null}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = React.useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
};