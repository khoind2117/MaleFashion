import { ProductVariantDto } from "../ProductVariant/ProductVariantDto";

export interface CartItemDto {
    quantity: number,
    productVariantId: number,
    productVariantDto: ProductVariantDto,
}