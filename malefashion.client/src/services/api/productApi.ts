import { PagedDto } from "../../models/dtos/PagedDto";
import { PagedProductDtos } from "../../models/dtos/Product/PagedProductDto";
import { ProductDetailDto } from "../../models/dtos/Product/ProductDetailDto";
import { Product } from "../../models/entities/Product";
import apiClient from "./apiClient";
import { AxiosResponse } from "axios";

const productApi = {
  getAll: (): Promise<AxiosResponse<Product[]>> => {
    return apiClient.get("/product");
  },

  getById: (id: number): Promise<AxiosResponse<ProductDetailDto>> => {
    return apiClient.get(`/product/${id}`);
  },

  create: (data: Product): Promise<AxiosResponse<Product>> => {
    return apiClient.post("/product", data);
  },

  update: (id: number, data: Product): Promise<AxiosResponse<Product>> => {
    return apiClient.put(`/product/${id}`, data);
  },

  soft_delete: (id: number): Promise<AxiosResponse<Product>> => {
    return apiClient.delete(`/product/soft/${id}`);
  },

  hard_delete: (id: number): Promise<AxiosResponse<Product>> => {
    return apiClient.delete(`/product/hard/${id}`);
  },

  getPaged: (): Promise<AxiosResponse<PagedDto<PagedProductDtos>>> => {
    return apiClient.get("/product/paged");
  },
};

export default productApi;