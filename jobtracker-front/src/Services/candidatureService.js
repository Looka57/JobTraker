import axios from 'axios'

const API_URL = 'https://localhost:7265/api/Candidatures'

export const candidatureService = {
    getAll() {
        return axios.get(API_URL).then(res => res.data)
    }
}