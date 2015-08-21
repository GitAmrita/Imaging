<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FaceDetectionWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Face Detection using Viola Jones Algorithm</h2>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h4>Source<h6>(ex:c:/temp/images)</h6></h4>
            <asp:TextBox ID="txtSource" runat="server" Text=""/>
        </div>
        <div class="col-md-4">
            <h4>FileType<h6>only images of selected type will be evaluated</h6></h4>
            <asp:DropDownList ID="ddlFileType" runat="server" Width="200px">
                    <asp:ListItem Text="jpg" Value="jpg"></asp:ListItem>
                    <asp:ListItem Text="jpeg" Value="jpeg"></asp:ListItem>
                    <asp:ListItem Text="png" Value="png"></asp:ListItem>
                    <asp:ListItem Text="gif" Value="gif"></asp:ListItem>
                </asp:DropDownList>
        </div>
        <div class="col-md-4">
             <h4>Target<h6>(ex:c:/temp/images/results)</h6></h4>
             <asp:TextBox ID="txtTarget" runat="server" Text=""/>
        </div>              
    </div>
    <div class="row">
        <div class="col-md-4">
            <h4>Select Search mode</h4>
            <asp:DropDownList ID="ddlSearchMode" runat="server" Width="200px"  AutoPostBack="false" onchange="CheckSupperession()">
                <asp:ListItem Text="Default" Value="1"></asp:ListItem>
                <asp:ListItem Text="Average" Value="2"></asp:ListItem>
                <asp:ListItem Text="No Overlap" Value="3"></asp:ListItem>
                <asp:ListItem Text="Single" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <h4>Supperession</h4>
            <asp:DropDownList ID="ddlSupperession" runat="server" Width="200px" Enabled="False">
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <h4>Select Scaling Mode</h4>
            <asp:DropDownList ID="ddlScalingMode" runat="server" Width="200px">
                <asp:ListItem Text="SmallerToGreater" Value="1"></asp:ListItem>
                <asp:ListItem Text="GreaterToSmaller" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="jombotron">
            <div>
                <asp:Button ID="btnSubmit" runat="server" Text="Let the magic begin" onclick="btnSubmit_Click" />      
            </div>
            <div>
                <asp:Label ID="lblError" runat="server" Width="100px"></asp:Label>
            </div>       
     </div>
    <script language ="javascript" type="text/javascript">
        function CheckSupperession() {
            var src = document.getElementById('MainContent_ddlSearchMode').value;
            var suppression = document.getElementById('MainContent_ddlSupperession');
            if (src == 2) {
                suppression.disabled = false;
            } else {
                suppression.disabled = true;
            }
        }
    </script>

</asp:Content>
    