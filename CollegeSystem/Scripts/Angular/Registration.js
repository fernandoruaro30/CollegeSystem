angular.module('AngularApp')
    .controller('RegistrationController', function ($scope, $window, SaveRegistrationService, GetRegistrationService, RemoveRegistrationService, GetStudentService, GetCourseService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.RegistrationDataArr = [];
        $scope.StudentDataArr = [];
        $scope.CourseDataArr = [];
        $scope.RegistrationData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save Registration
        $scope.Save = function () {
            SaveRegistrationService.SaveRegistration($scope.RegistrationData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Registration/Index';
                });
        };

        //Get Registrations data, if there is valid Id then bring specific Registration else all Registrations
        $scope.GetRegistration = function (Id, Editing) {
            $('.loader').modal('show');
            GetRegistrationService.GetRegistration(Id).then(function (reg) {
                GetStudentService.GetStudent(reg.data[0].Student.IdStudent).then(function (stu) {
                    $scope.StudentDataArr = stu.data;

                    GetCourseService.GetCourse(reg.data[0].Course.IdCourse).then(function (cou) {
                        $scope.CourseDataArr = cou.data;

                        if (reg.data.length > 0 && Editing) {
                            $scope.RegistrationData = reg.data[0];                            
                        }
                        else {
                            $scope.RegistrationDataArr = reg.data;
                        }
                    });
                });

                $('.loader').modal('hide');
            });
        };

        //Remove Registration by Id
        $scope.Remove = function (Id) {
                RemoveRegistrationService.RemoveRegistration(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetRegistration(0, false);
                });
        };
    })

    .factory('SaveRegistrationService', function ($http) {

        var fac = {};

        fac.SaveRegistration = function (d) {

            return $http({
                url: '/Registration/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetRegistrationService', function ($http) {

        var fac = {};

        fac.GetRegistration = function (id) {

            return $http({
                url: '/Registration/GetRegistration?IdRegistration=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveRegistrationService', function ($http) {

        var fac = {};

        fac.RemoveRegistration = function (id) {

            return $http({
                url: '/Registration/Delete?IdRegistration=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });