import store from '../store/vuex'
export default function authenticated({next,store}) {
    if(store.getters['auth/role']!='Administrator'){
        return next({
            name:'home'
        })
      }

      return next()
  }
  
