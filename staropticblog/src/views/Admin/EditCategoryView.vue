<template>
    <v-container v-if="mounted">
    <v-form ref="form" class='elevate-12' v-model='isFormValid' @submit.prevent="editCategory">
        <v-card class="text-center">
            <div class="text-center">

                <p class="text-h5 py-4">Edituj kategoriju - {{this.$route.params.id}}</p>
            </div>
            <v-card-text>
            <v-container>
                <v-row>
                
                <v-col cols="12">
                    <v-text-field
                    label="Naziv*"
                    required
                    v-model="category.name"
                    :rules="nameRule"
                    ></v-text-field>
                </v-col>

                <v-col cols="12">
                        <v-text-field
                        label="Opis*"
                        v-model="category.description"
                        :rules="descriptionRule"
                        required
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
                                
                <p class="text-danger text-center fidb fw-bold py-3">{{feedback}}</p>
                                
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
                Naziv: {{category.name}}
                </h4>
                <h5 class=" fw-bold mb-1 text-center">
                Opis: {{category.description}}
                </h5>
                <v-list-item-subtitle class="text-center fw-bold pt-3">Broj postova: {{category.postsCount}}</v-list-item-subtitle>
                <v-list-item-subtitle class="text-center fw-bold pt-3">Broj aktivnih postova: {{category.activePostsCount}}</v-list-item-subtitle>
                
                <v-list-item-subtitle class="text-center fw-bold pt-3">Ažurirana: <span v-if="category.updatedAt">{{category.updatedAt}}</span><span v-else><i class="fa-solid fa-xmark"></i></span></v-list-item-subtitle>
            </v-list-item-content>

         
            </v-list-item>

            <div class="text-center pt-2 pb-5">
            <v-btn class="bg-warning"
                to="/admin/categories"
                outlined
                text
            >
                Sve kategorije
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
        category:null,
        mounted:false,
        isFormValid:true,
        feedback:'',
        feedback2:'',
        nameRule: [
                  t => !!t || 'Naziv ne sme biti prazan',
                  l=> !l || l.length>2 || 'Naziv mora imati najmanje 3 karaktera.'
                ],
        descriptionRule: [
                  t => !!t || 'Opis ne sme biti prazan',
                  l=> !l || l.length>2 || 'Opis mora imati najmanje 3 karaktera.'
        ],
        
    
    }
},
methods: {
    resetF(){
      this.feedback2=''
    },
    editCategory(){
       // console.log(this.tag.name)
        //console.log(this.$route.params.id)
         axios.patch('categories/edit',{
               Name:this.category.name,
               Description:this.category.description,
               CategoryID:this.$route.params.id
              }
            )
            .then(()=>{
                this.feedback=''
                this.feedback2='Kategorija je uspešno ažurirana.'
                this.loadCategory()
            })
            .catch(err=>{
              console.log(err)
              this.feedback=err.response.data.message;
              //console.log(this.feedback)
        })
       
    },
        loadCategory(){
               axios.get(`categories/${this.$route.params.id}`)
             .then(response => {
                console.log(response.data);
                this.category=response.data;
                if(this.feedback2){
                  setTimeout(this.resetF, 1400)
                }
                this.mounted=true;
             
            })
             .catch(error => {
                if (error.response) {
                  //response status is an error code
              //console.log(error.response.status);
                if(error.response.status==404){
                    this.$router.push('/admin/categories')
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
    this.loadCategory()
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