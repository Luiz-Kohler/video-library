import moment from "moment"

export const FormataStringData = (data: Date) =>{
    return moment(data).format("DD/MM/YYYY");
  }