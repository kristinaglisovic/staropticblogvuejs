<template>
    <div v-if="mounted">
      
       
        <v-row>
           
            <v-col>

                <h2 class="text-center py-5">Komentari</h2>
                <v-card>
                        <div class="elevation-5">
                            <v-data-table
                            :headers="headers"
                            :items="comments.data"
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

                             <template v-slot:item.commentedAt="{ item }">
                                   <span>{{item.commentedAt | formatDate2}}</span>
                            </template>

                            <template v-slot:item.view="{ item }">
                                    <v-btn :disabled='!item.isActive' :to="{name:'post',params:{id:item.post.id}}" class="bg-primary text-warning" outlined><i class="fa-solid fa-eye"></i></v-btn>
                            </template>

                            <template v-slot:item.activation="{ item }">
                                <v-btn v-if='item.isActive' @click='CommentActivation(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-circle-minus"></i></v-btn>
                                <v-btn v-else @click='CommentActivation(item.id)' class="bg-success text-white ikona"><i class="fa-solid fa-check"></i></v-btn>

                            </template> 

                            <template v-slot:item.delete="{ item }">
                                   <v-btn @click='deleteComment(item.id)' class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>
                                  
                            </template> 
                            
                            </v-data-table> 
                            <div class="text-center pt-2">
                           <v-pagination class="pt-3"
                                v-model="currentPage"
                                :length="comments.pagesCount"
                                @input="fetchComments()"
                            ></v-pagination> 
                            <p class="py-3">Ukupno komentara: {{comments.totalCount}}</p>
                            </div>
                        </div>
                </v-card>
            </v-col>
        </v-row>
 
    

    <div>

     
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
                comments:'',
                mounted:false,
                currentPage:1,
               
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
                        text: 'Autor',
                        align: 'start',
                        sortable: false,
                        value: 'username',
                    },
                     {
                        text: 'Komentar',
                        align: 'start',
                        sortable: false,
                        value: 'text',
                    },
                      {
                        text: 'Pogledaj',
                        align: 'start',
                        sortable: false,
                        value: 'view',
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
                        value: 'commentedAt',
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
                    
                  
                    
                ],
            
            }
        },
        methods: {
           
            async fetchComments(){
                 try{
                   const response= await axios.get('comments?page='+this.currentPage+'&PerPage=8')
                   //console.log(response.data)
                   this.comments=response.data;
                   this.currentPage=response.data.currentPage;
                 }
                 catch(error){
                      console.log(error)
                 }
            },

          

              deleteComment(id){
                  //console.log(id)

               axios.delete('comments/'+id)
                    .then(()=>{
                     
                       this.fetchComments()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             },   

             CommentActivation(id){
                  //console.log(id)

               axios.patch('comments/'+id)
                    .then(()=>{
                     
                       this.fetchComments()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             }   
        },

          mounted: function() {
           
                this.mounted=true;
                    axios.get('comments?&PerPage=8')
                    .then(r=>{
                        //   console.log(r.data)
                           this.comments=r.data;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })
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