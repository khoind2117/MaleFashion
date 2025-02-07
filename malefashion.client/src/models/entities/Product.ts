import { SubCategory } from "./SubCategory";
import { ProductVariant } from "./ProductVariant";

export interface Product {
    id: number;
    name: string;
    slug: string;
    description?: string;
    price: number;
    createdAt: Date;
    updatedAt: Date;
    isDeleted: boolean;

    subCategoryId?: number;
    subCategory?: SubCategory;

    productVariants?: ProductVariant[];
}