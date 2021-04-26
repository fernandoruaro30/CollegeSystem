angular.module('AngularApp')
    .controller('TeacherController', function ($scope, $window, SaveTeacherService, GetTeacherService, RemoveTeacherService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.TeacherDataArr = [];
        
        $scope.TeacherData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save Teacher
        $scope.Save = function () {
            SaveTeacherService.SaveTeacher($scope.TeacherData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Teacher/Index';
                });
        };

        //Get Teachers data, if there is valid Id then bring specific Teacher else all Teachers
        $scope.GetTeacher = function (Id, Editing) {
            $('.loader').modal('show');
            GetTeacherService.GetTeacher(Id).then(function (d) {
                if (d.data.length > 0 && Editing) {
                    $scope.TeacherData = d.data[0];

                    $scope.TeacherData.Birthday = new Date(parseInt($scope.TeacherData.Birthday.replace(/\D/g, "")));

                }
                else {
                    $scope.TeacherDataArr = d.data;
                }

                //Fix date JSON format
                for (var ix = 0; ix < $scope.TeacherDataArr.length; ix++) {
                    $scope.TeacherDataArr[ix].Birthday = new Date(parseInt($scope.TeacherDataArr[ix].Birthday.replace(/\D/g, "")));
                }
                $('.loader').modal('hide');
            });
        };

        //Remove Teacher by Id
        $scope.Remove = function (Id) {
                RemoveTeacherService.RemoveTeacher(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetTeacher(0, false);
                });
        };
    })

    .factory('SaveTeacherService', function ($http) {

        var fac = {};

        fac.SaveTeacher = function (d) {

            return $http({
                url: '/Teacher/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetTeacherService', function ($http) {

        var fac = {};

        fac.GetTeacher = function (id) {

            return $http({
                url: '/Teacher/GetTeacher?IdTeacher=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveTeacherService', function ($http) {

        var fac = {};

        fac.RemoveTeacher = function (id) {

            return $http({
                url: '/Teacher/Delete?IdTeacher=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });