angular.module('AngularApp')
    .controller('CourseController', function ($scope, $window, SaveCourseService, GetCourseService, RemoveCourseService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.CourseDataArr = [];
        
        $scope.CourseData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save Course
        $scope.Save = function () {
            SaveCourseService.SaveCourse($scope.CourseData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Course/Index';
                });
        };

        //Get Courses data, if there is valid Id then bring specific Course else all Courses
        $scope.GetCourse = function (Id, Editing) {
            
            $('.loader').modal('show');

            GetCourseService.GetCourse(Id).then(function (d) {
                if (d.data.length > 0 && Editing) {
                    $scope.CourseData = d.data[0];
                }
                else
                {
                    $scope.CourseDataArr = d.data;
                }

                $('.loader').modal('hide');
            });
        };

        //Remove Course by Id
        $scope.Remove = function (Id) {
                RemoveCourseService.RemoveCourse(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetCourse(0, false);
                });
        };
    })

    .factory('SaveCourseService', function ($http) {

        var fac = {};

        fac.SaveCourse = function (d) {

            return $http({
                url: '/Course/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetCourseService', function ($http) {

        var fac = {};

        fac.GetCourse = function (id) {

            return $http({
                url: '/Course/GetCourse?IdCourse=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveCourseService', function ($http) {

        var fac = {};

        fac.RemoveCourse = function (id) {

            return $http({
                url: '/Course/Delete?IdCourse=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });