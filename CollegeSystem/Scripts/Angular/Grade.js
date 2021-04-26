angular.module('AngularApp')
    .controller('GradeController', function ($scope, $window, SaveGradeService, GetGradeService, RemoveGradeService, GetStudentService, GetSubjectService) {

        $scope.Message = '';
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.GradeDataArr = [];
        $scope.StudentDataArr = [];
        $scope.SubjectDataArr = [];
        
        $scope.GradeData = {};

        //Verifica se o formulário é valido (f1 é o nosso formulário)
        $scope.$watch('f1.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        //Save Grade
        $scope.Save = function () {
            SaveGradeService.SaveGrade($scope.GradeData).then(function (d) {
                    if (d.data.success) {
                        alert('Data saved successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $window.location.href = '/Grade/Index';
                });
        };

        //Get Grades data, if there is valid Id then bring specific Grade else all Grades
        $scope.GetGrade = function (Id, Editing) {
            $('.loader').modal('show');
            GetGradeService.GetGrade(Id).then(function (d) {
                if (d.data.length > 0 && Editing) {
                    $scope.GradeData = d.data[0];

                }
                else {
                    $scope.GradeDataArr = d.data;
                }
                                
                GetStudentService.GetStudent(Id).then(function (d) {
                    $scope.StudentDataArr = d.data;
                });

                GetSubjectService.GetSubject(Id).then(function (d) {
                    $scope.SubjectDataArr = d.data;
                });

                $('.loader').modal('hide');
            });
        };

        //Remove Grade by Id
        $scope.Remove = function (Id) {
                RemoveGradeService.RemoveGrade(Id).then(function (d) {
                    if (d.data.success) {
                        alert('Data removed successfull!');
                    }
                    else {
                        alert(d.data.errors);
                    }

                    $scope.GetGrade(0, false);
                });
        };
    })

    .factory('SaveGradeService', function ($http) {

        var fac = {};

        fac.SaveGrade = function (d) {

            return $http({
                url: '/Grade/Save',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    })

    .factory('GetGradeService', function ($http) {

        var fac = {};

        fac.GetGrade = function (id) {

            return $http({
                url: '/Grade/GetGrade?IdGrade=' + id,
                method: 'GET'
            });
        };

        return fac;
    })

    .factory('RemoveGradeService', function ($http) {

        var fac = {};

        fac.RemoveGrade = function (id) {

            return $http({
                url: '/Grade/Delete?IdGrade=' + id,
                method: 'GET',
                headers: { 'content-type': 'application/json' }
            });
        };

        return fac;
    });