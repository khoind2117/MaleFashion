
import { ProductVariantDto } from "../ProductVariant/ProductVariantDto";
import { SubCategoryDto } from "../SubCategory/SubCategoryDto";

export interface ProductDetailDto {
    id: number;
    name: string;
    slug: string;
    description: string;
    price: number;

    subCategoryDto: SubCategoryDto;

    productVariantDtos: ProductVariantDto[];
}