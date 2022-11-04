<template>
    <div v-if="mounted">
      
       
        <v-row>
           
            <v-col>

                <h2 class="text-center py-5">Postovi</h2>
                <v-card>
                        <div class="elevation-5">
                            <v-data-table
                            :headers="headers"
                            :items="posts.data"
                            hide-default-footer
                            class="elevation-1"
                          >
                          
                            
                           <template v-slot:item.isActive="{ item }">
                                   <span v-if="!item.isActive"><i class="fa-solid fa-xmark"></i></span>
                                   <span v-else><i class="fa-solid fa-check"></i></span>
                            </template>

                            <template v-slot:item.updatedAt="{ item }">
                                   <span v-if="item.updatedAt">{{item.updatedAt | formatDate2}}</span>
                                   <span v-else><i class="fa-solid fa-xmark"></i></span>
                            </template>

                             <template v-slot:item.createdAt="{ item }">
                                   <span>{{item.createdAt | formatDate2}}</span>
                            </template>

                            <template v-slot:item.view="{ item }">
                                    <v-btn :to="{name:'post',params:{id:item.id}}" :disabled="!item.isActive" class="bg-primary text-white" outlined><i class="fa-solid fa-eye"></i></v-btn>
                            </template>

                            <template v-slot:item.activation="{ item }">
                                <v-btn v-if='item.isActive' @click='PostActivation(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-circle-minus"></i></v-btn>
                                <v-btn v-else @click='PostActivation(item.id)' class="bg-success text-white ikona"><i class="fa-solid fa-check"></i></v-btn>

                            </template> 


                            <template v-slot:item.delete="{ item }">
                                   <v-btn @click='deletePost(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>
                                  
                            </template> 
                            <template v-slot:item.update="{ item }">
                                <!-- Button trigger modal -->
                             
                                    <v-btn :to="{name:'editPost',params:{id:item.id}}" color="blue darken-1" :disabled="!item.isActive">
                                    <i class="fa-solid fa-pen-to-square text-white"></i>
                           
                                    </v-btn>
                                
                                
                             
                            </template>
                           
                            
                            </v-data-table> 
                            <div class="text-center pt-2">
                           <v-pagination class="pt-3"
                                v-model="currentPage"
                                :length="posts.pagesCount"
                                @input="fetchPosts()"
                            ></v-pagination> 
                            <p class="py-3">Ukupno postova: {{posts.totalCount}}</p>
                            </div>
                        </div>
                </v-card>
            </v-col>
        </v-row>
 
    


      <div class="text-center pt-5">
              <h2 class="fw-bold pb-3">Dodavanje posta</h2>
            

            <v-btn link color="blue text-white" to="/admin/posts/createPost">Dodaj post</v-btn>
   
      </div>

    </div>

    
 


</template>

<script>
    import { mapGetters } from 'vuex';
     import axios from "axios"
    export default {
        name: "Categories",
        
        data() {
            return {
                posts:'',
                mounted:false,
                currentPage:1,
                dialog: false,
                firstName:'',
                lastName:'',
                username:'',
                email:'',
                role:'',
                postsCount:'',
                likesCount:'',
                commentsCount:'',
                isActive:'',
                image:'',
                id:'',
                isFormValid:true,
                feedback:'',
                 nameRule: [
                  t => !!t || 'Naziv ne sme biti prazan',
                  l=> !l || l.length>2 || 'Naziv mora imati najmanje 3 karaktera.'
                ],
                 descriptionRule: [
                  t => !!t || 'Opis ne sme biti prazan',
                  l=> !l || l.length>2 || 'Opis mora imati najmanje 3 karaktera.'
                ],
                headers: [
                    {
                        text: 'Id',
                        align: 'start',
                        sortable: false,
                        value: 'id',
                    },

                    {
                        text: 'Naslov',
                        align: 'start',
                        sortable: false,
                        value: 'title',
                    },
                    {
                        text: 'Opis',
                        align: 'start',
                        sortable: false,
                        value: 'description',
                    },
                    {
                        text: 'Autor',
                        align: 'start',
                        sortable: false,
                        value: 'author',
                    },
                     {
                        text: 'Broj slika',
                        align: 'start',
                        sortable: false,
                        value: 'imagesCount',
                    },
                    {
                        text: 'Broj lajkova',
                        align: 'start',
                        sortable: false,
                        value: 'likes',
                    },
                    {
                        text: 'Broj komentara',
                        align: 'start',
                        sortable: false,
                        value: 'commentsCount',
                    },
                  
                    {
                        text: 'Aktivan',
                        align: 'start',
                        sortable: false,
                        value: 'isActive',
                    },
                    {
                        text: 'Kreiran',
                        align: 'start',
                        sortable: false,
                        value: 'createdAt',
                    },
                    {
                        text: 'Ažuriran',
                        align: 'start',
                        sortable: false,
                        value: 'updatedAt',
                    },
                    {
                        text: 'Pogledaj',
                        align: 'start',
                        sortable: false,
                        value: 'view',
                    },
                    {
                        text: 'Obriši',
                        align: 'start',
                        sortable: false,
                        value: 'delete',
                    },
                    {
                        text: 'Aktivacija',
                        align: 'start',
                        sortable: false,
                        value: 'activation',
                    },
                    
                      {
                        text: 'Ažuriranje',
                        align: 'start',
                        sortable: false,
                        value: 'update',
                    },
                    
                ],
            
            }
        },
        methods: {
           
            async fetchPosts(){
                 try{
                   const response= await axios.get('posts?PerPage=7&page='+this.currentPage)
                   //console.log(response.data)
                   this.posts=response.data;
                   this.currentPage=response.data.currentPage;
                   this.mounted=true;
                 }
                 catch(error){
                      console.log(error)
                 }
            },

            

              deletePost(id){
                  //console.log(id)

               axios.delete('posts/'+id)
                    .then(()=>{
                     
                       this.fetchPosts()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             },   

              PostActivation(id){
                  //console.log(id)

               axios.patch('posts/'+id)
                    .then(()=>{
                     
                       this.fetchPosts()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             }   
        },

          mounted: function() {
           
            this.fetchPosts()
          
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
    
    .ikona{
        font-size: 18px;
    }
</style>