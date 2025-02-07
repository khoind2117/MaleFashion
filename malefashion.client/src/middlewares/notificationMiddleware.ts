import { Middleware } from "@reduxjs/toolkit";
import { addToCart, removeFromCart } from "../store/cartSlice";
import { toast } from 'react-toastify';

type CartActions = ReturnType<typeof addToCart> | ReturnType<typeof removeFromCart>;

const notificationMiddleware: Middleware = () => (next) => (action)  => {
    const result = next(action);    
    const typedAction = action as CartActions;

    const showToast = (message: string, type: "success" | "info" | "warning" | "error") => {
        toast(message, {
            position: "top-right",
            autoClose: 2000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            type: type,
            closeButton: false,
        });
    };

    switch (typedAction.type) {
        case addToCart.type:
            showToast("Sản phẩm đã thêm vào giỏ hàng!", "success");
            break;
        case removeFromCart.type:
            showToast("Sản phẩm đã bị xóa khỏi giỏ hàng!", "warning");
            break;
        default:
            break;
    }

    return result;
};

export default notificationMiddleware;
