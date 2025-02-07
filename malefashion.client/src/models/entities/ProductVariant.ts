import { Color } from "./Color";
import { Product } from "./Product";
import { Size } from "./Size";

export interface ProductVariant {
    id: number;
    stock: number;

    productId: number;
    product?: Product;

    colorId?: number;
    color?: Color;

    sizeId?: number;
    size?: Size;
}