import { ProductVariantDto } from "../ProductVariant/ProductVariantDto";

export interface CartItem {
    name: string,
    quantity: number,
    unitPrice: number,
    
    productVariantId: number,
    productVariantDto: ProductVariantDto
}