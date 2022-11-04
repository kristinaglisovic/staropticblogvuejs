<template>
  <div>
    <v-container fluid>
      <div class="row">
        <div
         class="col-md-3 col-sm-3 col-lg-3 col-xs-12"
        >
          <v-card outlined>
            <v-card-title class="fw-bold"><v-icon>mdi-filter</v-icon>Filteri</v-card-title>
            <v-divider class="my-1"></v-divider>
              <v-card-title class="pb-0"><i class="fa-solid fa-layer-group mr-1"></i>Kategorije</v-card-title>

              <v-divider></v-divider>
            <v-container class="pt-0" fluid>
 
                       <v-checkbox v-for="(c,ki) in categories" :key="'A'+ki" class='fw-bold'  v-if='c.isActive && c.activePostsCount>0' :label="c.name+'  ('+c.activePostsCount+')'" v-model="selected.categories" :value='c.id' hide-details dense  @change='filterCategoriesPosts($event)'></v-checkbox>
            </v-container>

            <v-divider class="my-1"></v-divider>
            
        
           
            <v-card-title class="pb-0"><i class="fa-solid fa-tag mr-1"></i> Tagovi</v-card-title>

              <v-divider></v-divider>
            <v-container class="pt-0" fluid>
                       <v-checkbox   v-for="(tag,ti) in tags" v-if='tag.isActive && tag.activePostsCount>0' :key="'B'+ti" class='fw-bold' @change='filterTagPosts($event)' v-model="selected.tags" :label="tag.name+'  ('+tag.activePostsCount+')'"  :value='tag.id'  hide-details dense></v-checkbox>

            </v-container>

            <v-divider class="my-1"></v-divider>
                <v-card-title class="fw-bold"><v-icon class="mr-2">fa-sort</v-icon>Sortiranje</v-card-title>
                 <v-select class="m-3"
                      :items="['Komentari rastuće', 'Komentari opadajuće',  'Lajkovi rastuće','Lajkovi opadajuće','Podrazumevano']"
                      label="Sortiraj po"
                      @change="sortBy"
                   >
                      <template v-slot:item="{ item, attrs, on }">
                      <v-list-item
                          v-bind="attrs"
                          v-on="on"
                      >
                          <v-list-item-title
                          :id="attrs['aria-labelledby']"
                          v-text="item"
                          ></v-list-item-title>
                      </v-list-item>
                          </template>
                  </v-select>
            
          </v-card>

        </div>


        <div
          class="col-md-9 col-sm-9 col-xs-12 col-lg-9"
        >

          <v-row dense>
              <v-col cols="12" sm="12">
                    <h1 class="text-center py-2">Blog postovi</h1>
                       <v-divider class="my-1"></v-divider>
              </v-col>
               
            

               <v-col cols="12" sm="12">
                    <v-text-field
                    id="search"
                    label="Pretražite postove"
                    prepend-icon="fa-magnifying-glass"
                    name="search"
                    v-model="search"
                    @click:prepend="searchPosts"
                    @keydown.enter.prevent="searchPosts"
                    ></v-text-field>
                    
                 </v-col>
               
          </v-row>

          <v-divider></v-divider>

          <div class="row text-center pt-4">
            <div class="col-md-6 col-sm-12 col-lg-6" v-if="mounted" :key="p.id" v-for="p in posts.data">
              <v-hover v-slot:default="{ hover }">
                <v-card
                  class="mx-auto"
                  color="grey lighten-4"
                 
                >
                  <v-img
                    class="white--text align-end"
                    :aspect-ratio="16/9"
                    v-bind:src="'http://localhost:5000/Images/'+p.images[0]"
                  >
                    <v-expand-transition>
                        <div
                        v-if="hover"
                        class="d-flex transition-fast-in-fast-out white darken-2 v-card--reveal display-3 white--text"
                        style="height: 100%;"
                      > 
                     
                     <router-link :to="{name:'post',params:{id:p.id}}">
                         <v-btn v-if="hover"  class="" outlined>Pogledaj</v-btn>
                     </router-link>
                       
                      </div>

                    </v-expand-transition>
                  </v-img>
                  <v-card-text class="siva">
                    <h3 class="pb-2 text-dark fw-bold">{{p.title}}</h3>
                    
                    <p class="py-2 fw-bold">{{p.description | truncate(30)}}</p>
                    <v-divider></v-divider>
                    <div class="d-flex justify-content-between flex-wrap">
                     <span><i class="fa-solid fa-user"></i> | {{p.author}}</span>   
                     <span><i class="fa-solid fa-clock"></i> | {{p.createdAt | formatDate}}</span>
                     <span><i class="fa-solid fa-comments"></i> | {{p.commentsCount}}</span>
                     <span><i class="fa-solid fa-heart"></i> | {{p.likes}}</span>
                    </div>

                  
                  <template v-if="authenticated">
                    <v-divider></v-divider>
                    <!-- <span v-if="like=='liked'"><i class="fa-solid fa-heart"></i></span> -->
                  <template  v-if="p.likesInfo.find(l => l.userId == user.id)">

                    <v-btn @click="likePost(p.id)" class="bg-danger"><i class="fa-solid fa-heart-crack"></i></v-btn>
                  </template>
                  <template v-else>
                    <v-btn @click="likePost(p.id)" class="bg-warning"><i class='fa-solid fa-heart'></i></v-btn>

                  </template>
                  </template>
              
                
               
                   
               
                   
                  </v-card-text>
                </v-card>
              </v-hover>
            
            </div>
          </div>
          <div class="text-center mt-12">
            <v-pagination
              v-model="currentPage"
              :length="posts.pagesCount"
              @input="fetchPosts(currentPage)"
              circle
             
            ></v-pagination>
            <p class="pt-5">Ukupno postova: {{totalCount}}</p>
          </div>
        </div>
      </div>
    </v-container>
  </div>
</template>
<script>
    import { mapGetters } from 'vuex';
    import axios from "axios"
import authenticated from '@/middlewares/admin';
    export default {
        data: () => ({
            search:'',
            posts:[],
            categories:[],
            tags:[],
            selected:{
              tags:[],
              categories:[]
            },
            sortOrder:'',
            currentPage:1,
            pagesCount:0,
            totalCount:0,
            delayTimer:null,
            like:'',
            dislike:'',
            mounted:false,
            baseUrl:'posts?isActive=true'
      }),
        methods: {

           async fetchPosts(page=1){

                const response=await axios.get(`${this.baseUrl}&page=${page}`,{
                  params: this.axiosParams
                });
                this.currentPage=response.data.currentPage;
                this.pagesCount=response.data.pagesCount;
                this.totalCount=response.data.totalCount;
            //    console.log(response)
                this.posts=response.data;
                this.mounted=true;
        
         },

         async fetchTags(){
           await axios.get("tags?perPage=-1")
            .then(response => {
           //   
                this.tags=response.data.data;
            })
            .catch(error => {
            console.log(error)
            });
         },

         async fetchCategories(){
            await axios.get("categories?perPage=-1")
            .then(response => {

                this.categories=response.data.data;
            })
            .catch(error => {
            console.log(error)
          });

         },


           likePost(id){
             //console.log(id)

               axios.patch('posts/like',{
                       UserId:this.user.id,
                       PostId:id,
                      }
                    )
                    .then(()=>{
                        this.fetchPosts(this.currentPage)
                       
                    })
                    .catch(err=>{
                      console.log(err)
                    })

           },
           

           searchPosts(){
            if(this.axiosParams.has('keyword')){
              this.axiosParams.delete('keyword')
            }
            this.axiosParams.append('keyword',this.search)
            
           
            this.fetchPosts()
          },
      

         
          filterTagPosts(e){
            if(this.axiosParams.getAll('TagsIds')){
              this.axiosParams.delete('TagsIds');
            }
            e.forEach(element => {
          
              this.axiosParams.append('TagsIds',element)
            });
           
            this.fetchPosts()
          },


          filterCategoriesPosts(e){
            if(this.axiosParams.getAll('CategoryIds')){
              this.axiosParams.delete('CategoryIds');
            }
            e.forEach(element => {
          
              this.axiosParams.append('CategoryIds',element)
            });
           
            this.fetchPosts()
          },
           sortBy(e){
            // this.mounted=false;
            switch(e) {
              case 'Komentari rastuće':
                 this.sortOrder='CommentsAsc'
                break;
              case 'Komentari opadajuće':
                this.sortOrder='CommentsDesc'
                break;
              case 'Lajkovi opadajuće':
              this.sortOrder='LikesDesc'
                break;
              case 'Lajkovi rastuće':
              this.sortOrder='LikesAsc'
                break;
              default:
              this.sortOrder=''
              break;
            }
       

            if(this.axiosParams.has('sortOrder')){
              this.axiosParams.delete('sortOrder')
            }
            this.axiosParams.append('sortOrder',this.sortOrder)
            
           
            this.fetchPosts()

           }
          
        },
        computed:{
      ...mapGetters({
            authenticated:'auth/authenticated',
            user:'auth/user'
          }),
          axiosParams() {
            const params = new URLSearchParams();
          
            return params;
         }
        
       },
         mounted: function(){
          this.fetchPosts();

          this.fetchCategories();

          this.fetchTags();

      
        },
        filters: {
            truncate: function(value) {
                if (value && value.length > 40) {
                    value = value.substring(0, 35) + '...';
                }
                return value
            }
        },
         
    }
</script>
<style scoped>
.siva{
  background-color: #7C99AC !important;
}

  .v-card--reveal {
    align-items: center;
    bottom: 0;
    justify-content: center;
    background-color: #c8c8c8b8  !important;
    position: absolute;
    width: 100%;
  }

  a{
    text-decoration: none !important;
  }
</style>