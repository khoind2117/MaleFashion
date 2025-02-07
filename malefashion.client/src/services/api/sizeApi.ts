import apiClient from "./apiClient";
import { AxiosResponse } from "axios";
import { Size } from '../../models/entities/Size';

const sizeApi = {
  getAll: (): Promise<AxiosResponse<Size[]>> => {
    return apiClient.get("/size");
  },

  getById: (id: number): Promise<AxiosResponse<Size>> => {
    return apiClient.get(`/size/${id}`);
  },

  create: (data: Size): Promise<AxiosResponse<Size>> => {
    return apiClient.post("/size", data);
  },

  update: (id: number, data: Size): Promise<AxiosResponse<Size>> => {
    return apiClient.put(`/size/${id}`, data);
  },

  delete: (id: number): Promise<AxiosResponse<Size>> => {
    return apiClient.delete(`/size/${id}`);
  }
};

export default sizeApi;