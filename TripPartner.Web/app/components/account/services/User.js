(function(module){

    module.factory('User', function(){
       function User(model) {
           this.Username = model.username,
           this.Email = model.email,
           this.Password = model.password;
       }
       return User;
    });

})(angular.module('app.account'));