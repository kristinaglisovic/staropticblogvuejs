import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/Front/HomeView.vue'
import LoginView from '../views/Front/LoginView.vue'
import RegisterView from '../views/Front/RegisterView.vue'
import BlogView from '../views/Front/BlogView.vue'
import PostView from '../views/Front/PostView.vue'
import AdminView from '../views/Admin/AdminView.vue'
import DashboardView from '../views/Admin/Dashboard.vue'
import TagsView from '../views/Admin/TagsView.vue'
import CategoriesView from '../views/Admin/CategoriesView.vue'
import FrontView from '../views/Front/Front.vue'
import UsersView from '../views/Admin/UsersView.vue'
import PostsView from '../views/Admin/PostsView.vue'
import CommentsView from '../views/Admin/CommentsView.vue'
import EditTagView from '../views/Admin/EditTagView.vue'
import EditCategoryView from '../views/Admin/EditCategoryView.vue'
import CreateAdminView from '../views/Admin/CreateAdminView.vue'
import EditAdminView from '../views/Admin/EditAdminView.vue'
import EditPostView from '../views/Admin/EditPostView.vue'
import EditUserView from '../views/Front/EditUserView.vue'
import CreatePostView from '../views/Admin/CreatePostView.vue'
import store from '../store/vuex'


Vue.use(VueRouter)

const routes = [
  {
    path: "",
    redirect: to => {
      return 'home'
    },
    pathMatch: "full"
  },
  {
    path: '/',
    name:'front',
    component:FrontView,
    children: [
      {
        path: '/home',
        name: 'home',
        component: HomeView,
      },
      {
        path: '/blog',
        name: 'blog',
        component: BlogView
      },
      {
        path: '/blog/:id',
        name: 'post',
        component: PostView
      },
      {
        path: '/login',
        name: 'login',
        component: LoginView,
        beforeEnter: (to, from, next) => {
          if(store.getters['auth/authenticated']){
            return next({
                name:'home'
            })
          }
    
          next()
        }
       
      },
      {
        path: '/editUser',
        name: 'editUserFront',
        component: EditUserView,
        beforeEnter: (to, from, next) => {
          if(!store.getters['auth/authenticated']){
            return next({
                name:'home'
            })
          }
    
          next()
        }
       
      },
      {
        path: '/register',
        name: 'register',
        component: RegisterView,
        beforeEnter: (to, from, next) => {
          if(store.getters['auth/authenticated']){
            return next({
                name:'home'
            })
          }
    
          next()
        }
       
      },
    ],
  },
  {
    path: '/admin',
    name: 'admin',
    component: AdminView,
    beforeEnter: (to, from, next) => {
      if(store.getters['auth/role']!='Administrator'){
        return next({
            name:'home'
        })
      }

      next()
    },
    redirect: to => {
      return 'admin/dashboard'
    },
    children: [
      {
        path: '/admin/dashboard',
        name: 'dashboard',
        component: DashboardView,
       
      },
      {
        path: '/admin/tags',
        name: 'tags',
        component: TagsView,
       
      },
      {
        path: '/admin/tag/:id',
        name: 'tag',
        component: EditTagView,
       
      },
      {
        path: '/admin/category/:id',
        name: 'category',
        component: EditCategoryView,
       
      },
      {
        path: '/admin/posts/edit/:id',
        name: 'editPost',
        component: EditPostView,
       
      },
      {
        path: '/admin/categories',
        name: 'categories',
        component: CategoriesView ,
       
      },
      {
        path: '/admin/users',
        name: 'users',
        component: UsersView ,
       
      },

      {
        path: '/admin/posts',
        name: 'posts',
        component: PostsView ,
       
      },
      {
        path: '/admin/comments',
        name: 'comments',
        component: CommentsView ,
       
      },
      {
        path: '/admin/users/createAdmin',
        name: 'createAdmin',
        component: CreateAdminView ,
       
      },
      {
        path: '/admin/users/:id',
        name: 'editAdmin',
        component: EditAdminView ,
       
      },
      {
        path: '/admin/posts/createPost',
        name: 'createPost',
        component: CreatePostView ,
       
      },
    ]
  },
 
  // {
  //   path: '/admin',
  //   name: 'admin',
  //   component: AdminView,
  //  // component: () => import('../components/Admin/Views/Dashboard')
  // },
  // {
  //   path: '/admin/dashboard',
  //   name: 'admin',
  //   component: () => import('../components/Admin/Views/Dashboard')
  // },
  
  { path: '*', redirect: '/' }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router


