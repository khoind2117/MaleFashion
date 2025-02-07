import { ColorDto } from "../Color/ColorDto";
import { SizeDto } from "../Size/SizeDto";

export interface ProductVariantDto {
    id: number;
    stock: number;

    productId: number;

    colorDto: ColorDto;

    sizeDto: SizeDto;
}