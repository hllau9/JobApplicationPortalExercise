$(function() {

	$('select[multiple].active.3col').multiselect({
	  columns: 3,
	  placeholder: 'Select Skills',
	  search: true,
	  searchOptions: {
		  'default': 'Search Skills'
	  },
	  selectAll: true
	});

});