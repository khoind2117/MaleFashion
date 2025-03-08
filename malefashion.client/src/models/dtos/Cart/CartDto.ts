import { CartItemDto } from "../CartItem/CartItemDto";

export interface CartDto {
    userId: string,
    basketId: string,
    lastUpdated: Date,
    cartItemDtos: CartItemDto[],
}