import apiClient from "./apiClient";
import { AxiosResponse } from "axios";
import { MainCategory } from '../../models/entities/MainCategory';

const mainCategoryApi = {
  getAll: (): Promise<AxiosResponse<MainCategory[]>> => {
    return apiClient.get("/maincategory");
  },

  getById: (id: number): Promise<AxiosResponse<MainCategory>> => {
    return apiClient.get(`/maincategory/${id}`);
  },

  create: (data: MainCategory): Promise<AxiosResponse<MainCategory>> => {
    return apiClient.post("/maincategory", data);
  },

  update: (id: number, data: MainCategory): Promise<AxiosResponse<MainCategory>> => {
    return apiClient.put(`/maincategory/${id}`, data);
  },

  delete: (id: number): Promise<AxiosResponse<MainCategory>> => {
    return apiClient.delete(`/maincategory/${id}`);
  }
};

export default mainCategoryApi;