import axios from 'axios'
import { toast } from 'react-toastify';


//SE FOR RODAR BACKEND LOCAL DEIXAR ESSA PORTA 5050
var porta = '5000';

const api = axios.create({
    baseURL: `http://localhost:${porta}`
})

api.interceptors.response.use((response) => response, (error) => {
      toast.error(error.response.data.Message);
  });

export default api;