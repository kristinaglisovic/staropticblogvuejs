import axios from "axios";
import store from './store/vuex'
axios.defaults.baseURL="http://localhost:3003/api/"
//axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token')}`
store.subscribe((mutation)=>{
    switch (mutation.type){
        case 'auth/SET_TOKEN':
        if(mutation.payload){
            axios.defaults.headers.common['Authorization'] = `Bearer ${mutation.payload}`
            localStorage.setItem('token',mutation.payload)
        }
        else{
            axios.defaults.headers.common['Authorization'] = null;
            localStorage.removeItem('token')
        }
        break;
    }
})