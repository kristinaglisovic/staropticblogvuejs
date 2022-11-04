<template>
            <v-container v-if="mounted">
            <v-form ref="form" class='elevate-12' v-model='isFormValid' @submit.prevent="editTag">
                <v-card class="text-center">
                    <div class="text-center">

                        <p class="text-h5 py-4">Edituj tag - {{this.$route.params.id}}</p>
                    </div>
                    <v-card-text>
                    <v-container>
                        <v-row>
                        
                        <v-col cols="12">
                            <v-text-field
                            label="Naziv*"
                            required
                            v-model="tag.name"
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
                    <div class="text-center py-3">
                    <v-spacer></v-spacer>
                  
                    <v-btn
                        color="blue darken-1"
                        type="submit"
                        :disabled="!isFormValid"
                    >
                        Ažuriraj
                    </v-btn>
                    </div>
                    <template v-if="feedback!=''">
                                        
                        <p class="text-danger text-center fidb fw-bold py-3">{{feedback.error}}</p>
                                        
                    </template>
                    <template v-if="feedback2!='' && feedback==''">
                                        
                            <p class="text-success text-center fidb fw-bold py-3">{{feedback2}}</p>
                                                        
                    </template>
                </v-card>
                </v-form> 

                 <v-card
                    class="mx-auto mt-5"
                    outlined
                >
                    <v-list-item three-line>
                    <v-list-item-content>
                        <div class="text-overline mb-4 text-center">
                        Pregled:
                        </div>
                        <h4 class=" fw-bold mb-1 text-center">
                        Naziv: {{tag.name}}
                        </h4>
                        <v-list-item-subtitle class="text-center fw-bold pt-3">Broj postova: {{tag.postsCount}}</v-list-item-subtitle>
                        <v-list-item-subtitle class="text-center fw-bold pt-3">Broj aktivnih postova: {{tag.activePostsCount}}</v-list-item-subtitle>
                        
                        <v-list-item-subtitle class="text-center fw-bold pt-3">Ažurirana: <span v-if="tag.updatedAt">{{tag.updatedAt}}</span><span v-else><i class="fa-solid fa-xmark"></i></span></v-list-item-subtitle>
                    </v-list-item-content>

                 
                    </v-list-item>

                    <div class="text-center pt-2 pb-5">
                    <v-btn class="bg-warning"
                        to="/admin/tags"
                        outlined
                        text
                    >
                        Svi tagovi
                    </v-btn>
                    </div>
                </v-card>
              


            </v-container>
              
 </template>

<script>
    import { mapGetters } from 'vuex';
     import axios from "axios"
     export default {
        name: "Tag",
        
        data() {
            return {
                tag:null,
                mounted:false,
                isFormValid:true,
                feedback:'',
                feedback2:'',
                nameRule: [
                  t => !!t || 'Naziv ne sme biti prazan',
                  l=> !l || l.length>2 || 'Naziv mora imati najmanje 3 karaktera.'
                ],
                
            
            }
        },
        methods: {
            editTag(){
               // console.log(this.tag.name)
                //console.log(this.$route.params.id)
                 axios.patch('tags/edit',{
                       Name:this.tag.name,
                       TagId:this.$route.params.id
                      }
                    )
                    .then(()=>{
                        this.feedback=''
                        this.feedback2='Tag je uspešno ažuriran.'
                        this.loadTag()
                    })
                    .catch(err=>{
                      console.log(err)
                      this.feedback=err.response.data.errors[0];
                      //console.log(this.feedback)
                })
               
            },
            resetF(){
                this.feedback2=''
                },

                loadTag(){
                       axios.get(`tags/${this.$route.params.id}`)
                     .then(response => {
                        console.log(response.data);
                        this.tag=response.data;
                        this.mounted=true;
                        if(this.feedback2){
                       setTimeout(this.resetF, 1400)
                       }
                     
                    })
                     .catch(error => {
                        if (error.response) {
                          //response status is an error code
                      //console.log(error.response.status);
                        if(error.response.status==404){
                            this.$router.push('/admin/tags')
                        }
  
                       }
                     else if (error.request) {
                      //response not received though the request was sent
                          console.log(error.request);
                      }
                       else {
                              //an error occurred when setting up the request
                      console.log(error.message);
                  }
               })
                
            }

        },

          mounted: function() {
            this.loadTag()
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