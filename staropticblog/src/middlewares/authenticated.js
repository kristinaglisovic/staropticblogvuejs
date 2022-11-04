import store from '../store/vuex'
export default function authenticated({next,store}) {
    if(store.getters['auth/authenticated']){
        return next({
            name:'home'
        })
      }

      return next()
  }
  
