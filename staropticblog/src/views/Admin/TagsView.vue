<template>
    <div v-if="mounted">
      
       
        <v-row>
           
            <v-col>

                <h2 class="text-center py-5">Tagovi</h2>
                <v-card>
                        <div class="elevation-5">
                            <v-data-table
                            :headers="headers"
                            :items="tags.data"
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
                              
                            <template v-slot:item.delete="{ item }">
                                   <v-btn @click='deleteTag(item.id)' :disabled="item.postsCount>0 || item.activePostsCount>0" class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>

                            </template> 


                            
                            <template v-slot:item.activation="{ item }">
                                <v-btn v-if='item.isActive' @click='TagActivation(item.id)' :disabled="item.postsCount>0 || item.activePostsCount>0" class="bg-danger text-white ikona"><i class="fa-solid fa-circle-minus"></i></v-btn>
                                <v-btn v-else @click='TagActivation(item.id)' class="bg-success text-white ikona"><i class="fa-solid fa-check"></i></v-btn>

                            </template> 


                            <template v-slot:item.update="{ item }">
                                <!-- Button trigger modal -->
                                    <v-btn :to="{name:'tag',params:{id:item.id}}" color="blue darken-1" :disabled="!item.isActive">
                                    <i class="fa-solid fa-pen-to-square text-white"></i>
                                    </v-btn>
                                
                                
                             
                            </template>

                      
                            

                 
                            </v-data-table> 
                            <div class="text-center pt-2">
                           <v-pagination class="pt-3"
                                v-model="currentPage"
                                :length="tags.pagesCount"
                                @input="fetchTags()"
                            ></v-pagination> 
                            <p class="py-3">Ukupno tagova: {{tags.totalCount}}</p>
                            </div>
                        </div>
                </v-card>
            </v-col>
        </v-row>
        <div>

                    <template>
                        <h3 class="text-center py-4">Kreiraj tag</h3>
            <v-row justify="center">
                <v-dialog
                v-model="dialog"
                persistent
                max-width="600px"
                >
                <template v-slot:activator="{ on, attrs }">
                    <v-btn
                    color="primary"
                    dark
                    v-bind="attrs"
                    v-on="on"
                    >
                    Dodaj tag
                    </v-btn>
                </template>
                <v-form ref="form" v-model='isFormValid' @submit.prevent="submitTag">
                <v-card>
                    <v-card-title>
                    <span class="text-h5">Kreiranje taga</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container>
                        <v-row>
                        
                        <v-col cols="12">
                            <v-text-field
                            label="Naziv*"
                            required
                            v-model="name"
                            :rules="nameRule"
                            ></v-text-field>
                        </v-col>
                       
                        <v-col
                            cols="12"
                            sm="6"
                        >
                        
                        </v-col>
                        </v-row>
                    </v-container>
                    <small>* označava obavezno polje</small>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="dialog = false"
                    >
                        Zatvori
                    </v-btn>
                    <v-btn
                        color="blue darken-1"
                        type="submit"
                        :disabled="!isFormValid"
                    >
                        Dodaj
                    </v-btn>
                    </v-card-actions>
                    <template v-if="feedback!=''">
                                        
                        <p class="text-danger text-center fidb fw-bold py-3">{{feedback.error}}</p>
                                        
                    </template>
                </v-card>
                </v-form>
                </v-dialog>
            </v-row>
            </template>
                </div>
                </div>
</template>

<script>
    import { mapGetters } from 'vuex';
     import axios from "axios"
     export default {
        name: "Tags",
        
        data() {
            return {
                tags:'',
                mounted:false,
                currentPage:1,
                dialog: false,
                name:'',
                isFormValid:true,
                feedback:'',
                nameRule: [
                  t => !!t || 'Naziv ne sme biti prazan',
                  l=> !l || l.length>2 || 'Naziv mora imati najmanje 3 karaktera.'
                ],
                headers: [
                    {
                        text: 'Id',
                        align: 'start',
                        sortable: false,
                        value: 'id',
                    },

                    {
                        text: 'Naziv',
                        align: 'start',
                        sortable: false,
                        value: 'name',
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
                        text: 'Aktivna',
                        align: 'start',
                        sortable: false,
                        value: 'isActive',
                    },
                    {
                        text: 'Kreirana',
                        align: 'start',
                        sortable: false,
                        value: 'createdAt',
                    },
                    {
                        text: 'Ažurirana',
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
           
            async fetchTags(){
                 try{
                   const response= await axios.get('tags?PerPage=8&page='+this.currentPage)
                   //console.log(response.data)
                   this.tags=response.data;
                   this.currentPage=response.data.currentPage;
                 }
                 catch(error){
                      console.log(error)
                 }
            },

              deleteTag(id){
                  //console.log(id)

               axios.delete('tags/'+id)
                    .then(()=>{
                     
                       this.fetchTags()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             },   
              submitTag() {
                 axios.post('tags',{
                       Name:this.name,
                      }
                    )
                    .then(()=>{
                        this.fetchTags()
                        this.feedback=''
                        this.dialog = false
                        this.$refs.form.reset()
            
                    })
                    .catch(err=>{
                      //console.log(err)
                      this.feedback=err.response.data.errors[0];
                      //console.log(this.feedback)
                    })
               
              },

              TagActivation(id){
                  //console.log(id)

               axios.patch('tags/'+id)
                    .then(()=>{
                     
                       this.fetchTags()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             }   
        },

          mounted: function() {
           
                this.mounted=true;
                this.fetchTags()
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