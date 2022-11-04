<template>
  <nav v-if="authenticated">
    
      <v-app-bar app color='red accent-3' class="text-white">
        
        <v-app-bar-nav-icon @click.stop="drawer = !drawer" class="d-block d-lg-none text-white"></v-app-bar-nav-icon>

          <v-spacer class="d-block d-lg-none"></v-spacer>
          <span class="mr-2 d-none d-md-block"><i class="fa-solid fa-gauge"></i></span>
          <v-toolbar-title>SO Blog - Admin Panel</v-toolbar-title>
          <v-spacer></v-spacer>

        <div class="d-none d-lg-block">
          <v-btn text class="ml-2 text-white" to="/home"><i class="fa-solid fa-house"></i></v-btn>
          <v-btn text class="ml-2 text-white" to="/blog"><i class="fa-solid fa-blog"></i></v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/dashboard">Kontrolna tabla</v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/tags">Tagovi</v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/categories">Kategorije</v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/users">Korisnici</v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/posts">Postovi</v-btn>
          <v-btn text class="ml-2 text-white" to="/admin/comments">Komentari</v-btn>

      
     
        
        </div> 
       
          <v-spacer class="d-none d-lg-block"></v-spacer>

          
       
        <template v-if="authenticated">
          <v-badge v-if="user.image!='No image'" bordered bottom color="green" dot offset-x="13" offset-y="13">
            <v-btn icon class="noBg" to="/edituser">
              <v-avatar size="35">
                
                  <v-img v-bind:src="'http://localhost:5000/Images/'+user.image"></v-img>
              </v-avatar>
              </v-btn>
          </v-badge>
           <template v-else>
              <v-badge bordered bottom color="green" dot offset-x="15" offset-y="15">
                <v-btn icon class="noBg" to="/edituser">
              <v-avatar size="40">
                  <v-icon>mdi-account-circle</v-icon>
              </v-avatar>
              </v-btn>
                </v-badge>
           
          </template>
                <v-icon @click='signOut' class="ml-2 text-white">mdi-logout</v-icon>
        </template>

          <template v-else>
              <v-avatar size="40">
                  <v-icon>mdi-account-circle</v-icon>
              </v-avatar>
           
          </template>
          
      </v-app-bar>
    <v-navigation-drawer class="drw"
      v-model="drawer"
    app
      left
      temporary
    >
      <v-list
        nav
        dense
      >
        <v-list-item-group
          active-class="deep-purple--text text--accent-4"
        >
 
         
              <v-list-item>
            <v-list-item-content>
              <v-list-item-title class="text-h6">
                {{user.firstName}} {{user.lastName}}
              </v-list-item-title>
              <v-list-item-subtitle>{{user.email}}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
         

           

       <v-list
          nav
          dense
        >

        
           <v-list-item link to="/admin/dashboard">
            <v-list-item-icon >
               <v-icon>fa-gauge</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Admin Panel</v-list-item-title>
          </v-list-item>

          
           <v-list-item link to="/admin/tags">
            <v-list-item-icon >
               <v-icon>fa-hashtag</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Tagovi</v-list-item-title>
          </v-list-item>
            <v-list-item link to="/admin/categories">
            <v-list-item-icon >
               <v-icon>fa-brands fa-elementor</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Kategorije</v-list-item-title>
          </v-list-item>

            <v-list-item link to="/admin/users">
            <v-list-item-icon >
               <v-icon>fa-users</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Korisnici</v-list-item-title>
          </v-list-item>

           <v-list-item link to="/admin/posts">
            <v-list-item-icon >
               <v-icon>fa-clipboard</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Postovi</v-list-item-title>
          </v-list-item>

            <v-list-item link to="/admin/comments">
            <v-list-item-icon >
               <v-icon>fa-comments</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Komentari</v-list-item-title>
          </v-list-item>
          

          <v-list-item to="/home" link>
            <v-list-item-icon>
              <v-icon>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Početna</v-list-item-title>
          </v-list-item>
          <v-list-item link to="/blog">
            <v-list-item-icon>
              <v-icon>mdi-format-bold</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Blog</v-list-item-title>
          </v-list-item>

   
          <v-list-item @click='signOut'>
            <v-list-item-icon >
               <v-icon>mdi-logout</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Odjavi se</v-list-item-title>
          </v-list-item>

            <v-list-item @click.stop="drawer = !drawer">
            <v-list-item-icon >
              <v-icon color="red">mdi-close</v-icon>
            </v-list-item-icon >
            <v-list-item-title class="titl">Zatvori</v-list-item-title>
          </v-list-item>
        </v-list>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </nav>
  
</template>

<script>
import { mapGetters,mapActions } from 'vuex';
export default {
    data:()=>({
        drawer:null,
    }),
    mounted(){
    },
    methods: {
        ...mapActions({
          signOutAction:'auth/signOut'
        }),

      signOut(){
        this.signOutAction().then(()=>{
           this.$router.replace({
            name:'login'
          })
        })
      }
    },
    computed:{
      ...mapGetters({
         authenticated:'auth/authenticated',
         user:'auth/user'
      })
    }
}
</script>

<style scoped>
.navigacija{
  background-color: #7C99AC !important;
}
.titl{
    color: red !important;
}
</style>