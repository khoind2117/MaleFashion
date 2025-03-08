import { ColorDto } from "../Color/ColorDto";
import { ProductDto } from "../Product/ProductDto";
import { SizeDto } from "../Size/SizeDto";

export interface ProductVariantDto {
    id: number;
    stock: number;

    productDto: ProductDto;

    colorDto: ColorDto;

    sizeDto: SizeDto;
}