<template v-if="mounted">
    <v-container fluid fill-height class="log">
      <v-row align="center" justify="center" class="py-5">
        <v-col cols="12" sm="8" md="8">
          <v-card class="elevation-12">
          
                <v-row align="center" justify="center">
                  <v-col cols="12" md="10" sm="12">
                    <v-card-text class="mt-12">
                      <h1 class="text-center">Kreiranje posta</h1>
                      <div class="text-center mt-4">
                        <i  class="fa-solid text-primary fw-bold h2 fa-blog"></i>
                     </div>

                      <v-form ref="form" @submit.prevent="handleSubmit" v-model='isFormValid' >
                    

                        <small>* označava obavezno polje</small>
                      <!-- ime -->
                     <v-text-field
                     class="mt-4"
                          label="Naslov*"
                          name="title"
                          v-model="title"
                          :rules="titleRule"
                          type="text"
                          prepend-icon="fa-solid fa-heading"
                          color="amber darken-1"
                        />
                        <v-text-field
                          label="Opis*"
                          name="description"
                          v-model="description"
                          :rules="descriptionRule"
                          type="text"
                          prepend-icon="fa-solid fa-d"
                          color="amber darken-1"
                        />
                        <v-text-field
                          label="Podnaslov 1*"
                          name="heading1"
                          :rules="heading1Rule"
                          v-model="heading1"
                          type="text"
                          prepend-icon="fa-solid fa-s"
                          color="amber darken-1"
                          class="mb-4"
                        />
                          <v-textarea id="editor1" v-model="text1" :rules="text1Rule" label="Tekst 1*" prepend-icon='fa-solid fa-font' outlined></v-textarea>

                        <v-text-field
                          label="Podnaslov 2"
                          name="heading2"
                          v-model="heading2"
                          :rules="heading2Rule"
                          type="text"
                          prepend-icon="fa-solid fa-s"
                          color="amber darken-1"
                          class="mt-4 mb-4"
                        />
                        <v-textarea id="editor1" v-model="text2" :rules="text2Rule" label="Tekst 2"  prepend-icon='fa-solid fa-font' outlined></v-textarea>
                        
                        <!-- Tagovi -->
                        
                        <v-card flat>
                            <v-card-text>
                                <p class="fw-bold h2 text-center">Tagovi *</p>
                            <v-container fluid>
                                <v-row>
                                <v-col
                                    cols="12"
                                    sm="12"
                                    md="12"
                                   
                                >
                                <v-select
                                    v-model="selected.tags"
                                    :items="tags"
                                    :rules="tagsRule"
                                    filled
                                    chips
                                    item-value="id" item-text="name"
                                    @change="setTags($event)"
                                    label="Tagovi"
                                    multiple
                                ></v-select>
                                  
                                </v-col>
                                
                                
                                </v-row>

                              
                               
                                
                            </v-container>
                            </v-card-text>
                        </v-card>

                        <!-- Kategorije -->
                        <v-card flat>
                            <v-card-text>
                                <p class="fw-bold h2 text-center">Kategorije *</p>
                            <v-container fluid>
                                <v-row>
                                <v-col
                                    cols="12"
                                    sm="12"
                                    md="12"
                                   
                                >
                                <v-select
                                    v-model="selected.categories"
                                    :items="categories"
                                    :rules="categoriesRule"
                                    filled
                                    chips
                                    item-value="id" item-text="name"
                                    @change="setCategories($event)"
                                    label="Kategorije"
                                    multiple
                                ></v-select>
                                  
                                </v-col>
                                
                                
                                </v-row>

                              
                               
                                
                            </v-container>
                            </v-card-text>
                        </v-card>

                          <!-- Slike -->
                          <v-card flat>
                            <v-card-text>
                                <p class="fw-bold h2 text-center">Slike *</p>
                            <v-container fluid>
                                <v-row>
                                <v-col
                                    cols="12"
                                    sm="12"
                                    md="12"
                                   
                                >
                                <v-select
                                    v-model="selected.images"
                                    :items="images"
                                    :rules="imagesRule"
                                    filled
                                    chips
                                    item-value="id"
                                    
                                    @change="setImages($event)"
                                    label="Slike"
                                    multiple
                                >
                                <template v-slot:item="{ item }">
                                <div class="p-2 d-flex align-items-center justify-content-center">
                                <v-img :src="'http://localhost:5000/Images/'+item.path" width="200px"></v-img>
                                </div>
                            </template>
                            </v-select>
                               
                                                                
                                </v-col>
                                
                                
                                </v-row>

                              
                               
                                
                            </v-container>
                            </v-card-text>
                        </v-card>


                     

                       
                           <template v-if="feedback"> 
   
                          <div class="pt-5">
                             <p class="text-danger text-center fw-bold">   {{ feedback.error }}</p>
                           </div>
                           </template>

                            <template v-if="feedback2"> 
                        
                        <div class="pt-5">
                            <p class="text-success text-center fw-bold">   {{ feedback2 }}</p>
                          </div>
                          </template>
                          <div class="text-center mt-3"> 
                           <v-btn type="submit" class="my-2 m-1" rounded color="amber darken-1" :disabled="!isFormValid">Kreiraj</v-btn>
                         
                           <v-btn to="/admin/posts"  class="my-2" rounded color="green text-white m-1 darken-1">POSTOVI</v-btn>
                       </div>
                      </v-form>
                    </v-card-text>
                                             
                   
                  </v-col>
                 
                </v-row>
                          
        
            
          </v-card>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import axios from 'axios';
import {required,email,sameAs,minLength,maxLength,helpers} from 'vuelidate/lib/validators'
import { mapGetters } from 'vuex';



export default {
data: () => ({
    mounted:false,
    isFormValid:true,
   title:'',
   description:'',
   heading1:'',
   heading2:'',
   text1:'',
   text2:'',
   feedback:'',
   feedback2:'',
   categories:null,
   tags:null,
   images:null,
   selected:{
              tags:[],
              categories:[],
              images:[]
    },
    heading1Rule: [
                  t => !!t || 'Podnaslov ne sme biti prazan.',
                  l=> !l || l.length>=3 || 'Podnaslov mora imati najmanje 3 karaktera.'
                ],


  heading2Rule: [
                  l=> !l || l.length>=3 || 'Podnaslov mora imati najmanje 3 karaktera.'
                ],

  titleRule: [
                  t => !!t || 'Naslov ne sme biti prazan.',
                  l=> !l || l.length>=3 || 'Naslov mora imati najmanje 3 karaktera.'
              ],
  descriptionRule: [
                  t => !!t || 'Opis ne sme biti prazan.',
                  l=> !l || l.length>=5 || 'Opis mora imati najmanje 5 karaktera.'
  ],

  text1Rule: [
                  t => !!t || 'Tekst ne sme biti prazan.',
                  l=> !l || l.length>=3 || 'Tekst mora imati najmanje 3 karaktera.',
                  l=> !l || l.length<=400 || 'Tekst može imati najviše 400 karaktera.'],

                  text2Rule: [
                  l=> !l || l.length>=3 || 'Tekst mora imati najmanje 3 karaktera.',
                  l=> !l || l.length<=400 || 'Tekst može imati najviše 400 karaktera.'],


                  tagsRule: [
                  // t => !!t || 'Tekst ne sme biti prazan',
                  l=> !l || l.length>=1 || 'Morate selektovati bar 1 tag.'],
                  
                  categoriesRule: [
                  // t => !!t || 'Tekst ne sme biti prazan',
                  l=> !l || l.length>=1 || 'Morate selektovati bar 1 kategoriju.'],

                  imagesRule: [
                  // t => !!t || 'Tekst ne sme biti prazan',
                  l=> !l || l.length>=1 || 'Morate selektovati bar 1 sliku.'],

                  

}),
 
 methods: {

 
  resetF(){
      this.feedback2=''
    },
      setCategories(e){
        this.selected.categories=e;
      },
      setTags(e){
        this.selected.tags=e;
       // console.log(this.selected.tags)
      },
      setImages(e){
        console.log(e)
        this.selected.images=e;
       // console.log(this.selected.tags)
      },

      async fetchTags(){
           await axios.get("tags?perPage=-1&isActive=true")
            .then(response => {
           //   
                this.tags=response.data.data;
                this.mounted=true;
            })
            .catch(error => {
            console.log(error)
            });
         },

     async fetchCategories(){
            await axios.get("categories?perPage=-1&isActive=true")
            .then(response => {

                this.categories=response.data.data;
                this.mounted=true;
            })
            .catch(error => {
            console.log(error)
          });

         },

         async fetchImages(){
            await axios.get("images?perPage=-1&isActive=true&postsOnly=true")
            .then(response => {
                //console.log(response.data.data)
                this.images=response.data.data;
                this.mounted=true;
            })
            .catch(error => {
            console.log(error)
          });

         },

     handleSubmit(){
      axios.post('posts',{
                       Title:this.title,
                       Description:this.description,
                       Heading1:this.heading1,
                       Heading2:this.heading2,
                       Text1:this.text1,
                       Text2:this.text2,
                       CategoriesIds:this.selected.categories,
                       TagsIds:this.selected.tags,
                       ImagesIds:this.selected.images,
                       UserId:this.user.id
                      }
                    )
                    .then(()=>{
                        this.feedback2='Uspešno ste kreirali post.'
                        this.feedback=''
                        if(this.feedback2){
                            setTimeout(this.resetF, 1400)
                            this.$refs.form.reset()
                         }
            
                    })
                    .catch(err=>{
                      console.log(err)
                      this.feedback=err.response.data.errors[0];
                      //console.log(this.feedback)
      })
        
     },
     

 },
 
 mounted() {
    this.fetchTags();
    this.fetchCategories();
    this.fetchImages();

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
a{
text-decoration: none;
}

.siva{
  color:#7C99AC;
}


</style>