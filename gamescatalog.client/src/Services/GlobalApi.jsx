import axios from 'axios';

const axiosCrete = axios.create({
    baseURL:'http://localhost:7200/api/App',
});

const getFilters=axiosCrete.get('/filters');

export default {
    getFilters,
}