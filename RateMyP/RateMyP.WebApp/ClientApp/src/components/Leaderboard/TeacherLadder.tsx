import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';
import * as CoursesStore from '../../store/Courses';
import MUIDataTable, { SelectableRows } from 'mui-datatables';
