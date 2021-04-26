angular.module('AngularApp')
    .controller('StudentController', function ($scope, $window, SaveStudentService, GetStudentService, RemoveStudentService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.StudentDataArr = [];
        
        $scope.StudentData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save student
        $scope.Save = function () {
            SaveStudentService.SaveStudent($scope.StudentData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Student/Index';
                });
        };

        //Get students data, if there is valid Id then bring specific student else all students
        $scope.GetStudent = function (Id, Editing) {
            $('.loader').modal('show');
            GetStudentService.GetStudent(Id).then(function (d) {
                if (d.data.length > 0 && Editing) {
                    $scope.StudentData = d.data[0];

                    $scope.StudentData.Birthday = new Date(parseInt($scope.StudentData.Birthday.replace(/\D/g, "")));
                    
                }
                else {
                    $scope.StudentDataArr = d.data;
                }

                //Fix date JSON format
                for (var ix = 0; ix < $scope.StudentDataArr.length; ix++)
                {
                    $scope.StudentDataArr[ix].Birthday = new Date(parseInt($scope.StudentDataArr[ix].Birthday.replace(/\D/g, "")));
                }

                $('.loader').modal('hide');
            });
        };

        //Remove Student by Id
        $scope.Remove = function (Id) {
                RemoveStudentService.RemoveStudent(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetStudent(0, false);
                });
        };
    })

    .factory('SaveStudentService', function ($http) {

        var fac = {};

        fac.SaveStudent = function (d) {

            return $http({
                url: '/Student/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetStudentService', function ($http) {

        var fac = {};

        fac.GetStudent = function (id) {

            return $http({
                url: '/Student/GetStudent?IdStudent=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveStudentService', function ($http) {

        var fac = {};

        fac.RemoveStudent = function (id) {

            return $http({
                url: '/Student/Delete?IdStudent=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });