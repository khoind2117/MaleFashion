import { MainCategory } from "./MainCategory";
import { Product } from "./Product";

export interface SubCategory {
    id: number;
    name: string;
    slug: string;

    mainCategoryId?: number;
    mainCategory?: MainCategory;
    
    products?: Product[];
}