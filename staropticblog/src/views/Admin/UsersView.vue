<template>
    <div v-if="mounted">
      
       
        <v-row>
           
            <v-col>

                <h2 class="text-center py-5">Korisnici</h2>
                <v-card>
                        <div class="elevation-5">
                            <v-data-table
                            :headers="headers"
                            :items="users.data"
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
                            <template v-slot:item.image="{ item }">
                                <div class="p-2">
                                <v-img v-if=' item.image != "No image"' :src="'http://localhost:5000/Images/'+item.image" width="80px"></v-img>
                                <span v-else><i class="fa-solid fa-xmark"></i></span>
                                </div>
                            </template>

                            <template v-slot:item.activation="{ item }">
                                <v-btn v-if='item.isActive' @click='UserActivation(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-circle-minus"></i></v-btn>
                                <v-btn v-else @click='UserActivation(item.id)' class="bg-success text-white ikona"><i class="fa-solid fa-check"></i></v-btn>

                            </template> 

                            <template v-slot:item.delete="{ item }">
                                   <v-btn v-if="item.postsCount==0" @click='deleteUser(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>
                                   
                                   <v-btn v-else @click='deactivateUserAndItsPosts(item.id)' :disabled='!item.isActive && item.activePostsCount==0 && item.likesCount==0 && item.commentsCount==0' class="bg-warning text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>
                                   
                            </template> 

                            <template v-slot:item.update="{ item }">
                                <!-- Button trigger modal -->
                                    <v-btn   :to="{name:'editAdmin',params:{id:item.id}}" :disabled='!item.isActive' color="blue darken-1">
                                    <i class="fa-solid fa-pen-to-square text-white"></i>
                                    </v-btn>
                                
                                
                             
                            </template>
                            
                            <template v-slot:item.role="{ item }">
                                <span v-if="item.role=='User'">Korisnik</span>
                                <span v-else>{{item.role}}</span>
                            </template>
                            
                            </v-data-table> 
                            <div class="text-center pt-2">
                           <v-pagination class="pt-3"
                                v-model="currentPage"
                                :length="users.pagesCount"
                                @input="fetchUsers()"
                            ></v-pagination> 
                            <p class="py-3">Ukupno korisnika: {{users.totalCount}}</p>
                            </div>
                        </div>
                </v-card>
            </v-col>
        </v-row>
 
    

    <div class="text-center pt-5">
      <h2 class="fw-bold pb-3">Dodavanje admina</h2>
      <v-btn link color="blue text-white" to="/admin/users/createAdmin">Dodaj admina</v-btn>
      
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
                users:'',
                mounted:false,
                currentPage:1,
                dialog: false,
                headers: [
                    {
                        text: 'Id',
                        align: 'start',
                        sortable: false,
                        value: 'id',
                    },

                    {
                        text: 'Ime',
                        align: 'start',
                        sortable: false,
                        value: 'firstName',
                    },
                    {
                        text: 'Prezime',
                        align: 'start',
                        sortable: false,
                        value: 'lastName',
                    },
                    {
                        text: 'Korisničko ime',
                        align: 'start',
                        sortable: false,
                        value: 'username',
                    },
                     {
                        text: 'Slika',
                        align: 'start',
                        sortable: false,
                        value: 'image',
                    },
                    {
                        text: 'Email',
                        align: 'start',
                        sortable: false,
                        value: 'email',
                    },
                    {
                        text: 'Uloga',
                        align: 'start',
                        sortable: false,
                        value: 'role',
                    },
                    {
                        text: 'Br. postova',
                        align: 'start',
                        sortable: false,
                        value: 'postsCount',
                    },
                    {
                        text: 'Br. aktivnih postova',
                        align: 'start',
                        sortable: false,
                        value: 'activePostsCount',
                    },
                    {
                        text: 'Br. lajkova',
                        align: 'start',
                        sortable: false,
                        value: 'likesCount',
                    },
                     {
                        text: 'Br. komentara',
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
                        text: 'Registrovan',
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
           
            async fetchUsers(){
                 try{
                   const response= await axios.get('users?PerPage=6&page='+this.currentPage)
                   //console.log(response.data)
                   this.users=response.data;
                   this.currentPage=response.data.currentPage;
                 }
                 catch(error){
                      console.log(error)
                 }
            },

        

              deleteUser(id){
                  //console.log(id)

               axios.delete('users/'+id)
                    .then(()=>{
                     
                       this.fetchUsers()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             },   

             deactivateUserAndItsPosts(id){
              axios.patch('users/deactivate/'+id)
                    .then(()=>{
                     
                       this.fetchUsers()
                    })
                    .catch(err=>{
                      console.log(err)
                    })
             },

             UserActivation(id){
                  //console.log(id)

               axios.patch('users/'+id)
                    .then(()=>{
                     
                       this.fetchUsers()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             }   
        },

          mounted: function() {
           
            this.fetchUsers();
            this.mounted=true;
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