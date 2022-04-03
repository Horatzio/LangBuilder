import axios from "axios";
import { TranspilerModel } from "./transpiler-model";

const apiUrl = "http://localhost:7402";

export const TranspilerService = {
  saveTranspiler: async (model: TranspilerModel): Promise<void> => {
    await axios.post(`${apiUrl}/transpiler/save`, model);
  },
};
