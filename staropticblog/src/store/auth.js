import axios from "axios";
import jwt_decode from "jwt-decode";
export default {
    namespaced:true,
    state:{
        token: null,
        user:null,
        role:null,
        like:null
    },
    actions:{
       async signIn({dispatch}, credentials){
        
         let response=await axios.post("token", credentials);

        return dispatch('attempt',response.data.token)

        },

        async attempt({commit,state},token){
           if(token){
               commit('SET_TOKEN',token)
           }
           if(!state.token){
             return
           }


           try{
                
                var decoded = jwt_decode(token);

                var role=decoded.Role;
                //console.log(role)       
           
                let response=await axios.get(`users/${decoded.UserId}`);
        
                commit('SET_USER',response.data)
                commit('SET_ROLE',role)
                



           }
           catch (e){
            commit('SET_TOKEN',null)
            commit('SET_USER',null)
            commit('SET_ROLE',null)
           }
        },
        signOut({commit}){
            commit('SET_TOKEN',null)
            commit('SET_USER',null)
            commit('SET_ROLE',null)
        }
    },
    mutations:{
        SET_TOKEN(state,token){
              state.token=token
        },

        SET_USER(state,data){
            state.user=data
       },

        SET_ROLE(state,data){
            state.role=data
         }

    },

    getters:{

        authenticated(state) {
            return state.token && state.user;
        },

        user(state){
            return state.user
        },

        role(state){
            return state.role
        }
        



    }
 

};

