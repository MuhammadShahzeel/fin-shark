// api.ts
import axios from "axios";
import { handleError } from "../helpers/ErrorHandler";
import type { CommentPost } from "../models/Comment";


// Axios instance
const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "https://localhost:7165/api/",
});


export const commentPostAPI = async (title: string,
  content: string,
  symbol: string) => {
  try {
    const response = await apiClient.post<CommentPost>(`/comment/${symbol}`, {
         title,
      content, 
    });
    return response;
  } catch (error) {
    handleError(error);
  }
};


