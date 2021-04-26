angular.module('AngularApp')
    .controller('SubjectController', function ($scope, $window, SaveSubjectService, GetSubjectService, RemoveSubjectService, GetTeacherService, GetCourseService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.SubjectDataArr = [];        
        $scope.TeacherDataArr = [];
        $scope.CourseDataArr = [];
        $scope.SubjectData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save Subject
        $scope.Save = function () {
            SaveSubjectService.SaveSubject($scope.SubjectData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Subject/Index';
                });
        };

        //Get Subjects data, if there is valid Id then bring specific Subject else all Subjects
        $scope.GetSubject = function (Id, Editing) {
            $('.loader').modal('show');
            GetSubjectService.GetSubject(Id).then(function (d) {
                if (d.data.length > 0 && Editing) {
                    $scope.SubjectData = d.data[0];

                }
                else {
                    $scope.SubjectDataArr = d.data;
                }

                //Fix date JSON format
                for (var ix = 0; ix < $scope.SubjectDataArr.length; ix++) {
                    $scope.SubjectDataArr[ix].Teacher.Birthday = new Date(parseInt($scope.SubjectDataArr[ix].Teacher.Birthday.replace(/\D/g, "")));
                }

                GetTeacherService.GetTeacher(Id).then(function (d) {
                    $scope.TeacherDataArr = d.data;
                });
                               
                GetCourseService.GetCourse(Id).then(function (d) {
                    $scope.CourseDataArr = d.data;
                });

                $('.loader').modal('hide');
            });
        };

        //Remove Subject by Id
        $scope.Remove = function (Id) {
                RemoveSubjectService.RemoveSubject(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetSubject(0, false);
                });
        };
    })

    .factory('SaveSubjectService', function ($http) {

        var fac = {};

        fac.SaveSubject = function (d) {

            return $http({
                url: '/Subject/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetSubjectService', function ($http) {

        var fac = {};

        fac.GetSubject = function (id) {

            return $http({
                url: '/Subject/GetSubject?IdSubject=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveSubjectService', function ($http) {

        var fac = {};

        fac.RemoveSubject = function (id) {

            return $http({
                url: '/Subject/Delete?IdSubject=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });