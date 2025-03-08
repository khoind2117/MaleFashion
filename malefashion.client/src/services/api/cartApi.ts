import { CartDto } from "../../models/dtos/Cart/CartDto";
import { CartItemRequestDto } from "../../models/dtos/CartItem/CartItemRequestDto";
import apiClient from "./apiClient";
import { AxiosResponse } from "axios";

const cartApi = {                  
  getCart: (): Promise<AxiosResponse<CartDto>> => {
    return apiClient.get("/cart/get");
  },
  mergeCart: (): Promise<AxiosResponse<boolean>> => {
    return apiClient.post("/cart/merge", { withCredentials: true });
  },
  addToCart: (cartItemRequestDto: CartItemRequestDto): Promise<AxiosResponse<boolean>> => {
    return apiClient.post("/cart/add", cartItemRequestDto);
  },
  removeFromCart: (productVariantId: number): Promise<AxiosResponse<boolean>> => {
    return apiClient.delete(`/cart/remove/${productVariantId}`);
  }
};

export default cartApi;
