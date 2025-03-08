import { AxiosResponse } from "axios";
import { ProductVariantDto } from "../../models/dtos/ProductVariant/ProductVariantDto";
import apiClient from "./apiClient";

const productVariantApi = {
    details: (productVariantIds: number[]): Promise<AxiosResponse<ProductVariantDto[]>> => {
        return apiClient.post("/productVariant/details", productVariantIds);
    }
}

export default productVariantApi;