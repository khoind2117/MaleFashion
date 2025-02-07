import { Color } from "../../entities/Color";

export interface PagedProductDtos {
    id: number;
    name: string;
    slug: string;
    price: number;

    colors: Color[];
}