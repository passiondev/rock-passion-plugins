﻿// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rock;
using Rock.Web.UI.Controls;

using Humanizer;

namespace com.passioncitychurch.familyregistration
{
    /// <summary>
    /// Report Filter control
    /// </summary>
    [ToolboxData("<{0}:PassionPreRegistrationChildren runat=server></{0}:PassionPreRegistrationChildren>")]
    public class PassionPreRegistrationChildren : PreRegistrationChildren
    {
        private LinkButton _lbAddChild;
        private string childType;

        /// <summary>
        /// Gets the group member rows.
        /// </summary>
        /// <value>
        /// The group member rows.
        /// </value>
        public List<PassionChildRow> PassionChildRows
        {
            get
            {
                var rows = new List<PassionChildRow>();
                foreach ( Control control in Controls )
                {
                    if ( control is PassionChildRow)
                    {
                        var newGroupMemberRow = control as PassionChildRow;
                        if ( newGroupMemberRow != null )
                        {
                            rows.Add( newGroupMemberRow );
                        }
                    }
                }

                return rows;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            _lbAddChild = new LinkButton();
            Controls.Add( _lbAddChild );
            _lbAddChild.ID = "_btnAddChild";
            _lbAddChild.Click += lbAddChild_Click;
            _lbAddChild.AddCssClass("add btn btn-xs btn-default");
            _lbAddChild.CausesValidation = false;

            var iAddFilter = new HtmlGenericControl( "div" );
            iAddFilter.AddCssClass( "fa fa-user" );
            _lbAddChild.Controls.Add( iAddFilter );

            var spanAddFilter = new HtmlGenericControl( "span" );
            spanAddFilter.InnerHtml = " Add Child";
            _lbAddChild.Controls.Add( spanAddFilter );
        }

        /// <summary>
        /// Handles the Click event of the lbAddChild control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void lbAddChild_Click( object sender, EventArgs e )
        {
            if ( AddChildClick != null )
            {
                AddChildClick( this, e );
            }
        }

        /// <summary>
        /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderControl( HtmlTextWriter writer )
        {
            if ( this.Visible )
            {
                int i = 0;
                foreach ( Control control in Controls )
                {
                    if ( control is PassionChildRow)
                    {
                        i++;
                        var childRow = control as PassionChildRow;
                        childRow.Caption = $"{i.ToOrdinalWords().Titleize()} Child";
                        childRow.RenderControl( writer );
                    }
                }

                writer.AddAttribute( HtmlTextWriterAttribute.Class, "text-center");
                writer.RenderBeginTag( HtmlTextWriterTag.Div );
                _lbAddChild.RenderControl( writer );
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Clears the rows.
        /// </summary>
        public void ClearRows()
        {
            for ( int i = Controls.Count - 1; i >= 0; i-- )
            {
                if ( Controls[i] is PassionChildRow)
                {
                    Controls.RemoveAt( i );
                }
            }
        }

        /// <summary>
        /// Occurs when [add group member click].
        /// </summary>
        public event EventHandler AddChildClick;

    }
}