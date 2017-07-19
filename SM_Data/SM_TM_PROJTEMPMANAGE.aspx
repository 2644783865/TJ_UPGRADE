<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_TM_PROJTEMPMANAGE.aspx.cs"  MasterPageFile="~/Masters/SMBaseMaster.Master" Inherits="ZCZJ_DPF.SM_Data.SM_TM_PROJTEMPMANAGE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">



<script src="SM_JS/superTables.js" type="text/javascript"></script>
 
<link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    
<link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

<link rel="stylesheet" type="text/css" href="stylesheets/superTables.css" />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>


    <style type="text/css">
    .tdclass
        {
            border-width: 0px 1px 1px 0px; 
            border-style: solid; 
            border-color: #adadad;
        }
    </style>

<script type="text/javascript" language="javascript">


function ActivateAlertDiv(visstring, elem, msg)
{
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}


function viewCondition()
{
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}

</script>
     
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
      
    <table width="98%">
        <tr>
            <td align="left" style="width:auto">
             <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示" Visible="false" OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false" ></asp:Label>
                &nbsp;&nbsp;
            </td>
            <td style="width:auto" >
            
               <asp:CheckBox runat="server" ID="MyTask" Text="我的任务" AutoPostBack="true" Checked="true" OnCheckedChanged="MyTask_CheckedChanged" /> 
              
              </td>
              <td style="width:auto">               
               <asp:RadioButtonList ID="RadioButtonListState" runat="server" OnSelectedIndexChanged="RadioButtonListState_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>                                
                    <asp:ListItem Text="待提交" Value="1"></asp:ListItem>
                    <asp:ListItem Text="待审批" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已审批" Value="3"></asp:ListItem>                          
               </asp:RadioButtonList>                                    
            </td>
            <td align="right" style="width:auto">
                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()"/>&nbsp;&nbsp;
                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" 
                    TargetControlID="btnShowPopup" PopupControlID="UpdatePanelCondition"  Drag="false"  
                    Enabled="True"  DynamicServicePath=""  Y="30" >
                </asp:ModalPopupExtender>                 
               
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    
    <asp:Panel ID="PanelCondition" runat="server" Width="98%" style="display:none" > 
    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
    <ContentTemplate> 
    
     <table width="96%" style="background-color:#CCCCFF; border:solid 1px black;">
         <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
                        &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
               <asp:Button ID="QueryButton" runat="server" Text="查询"  OnClick="QueryButton_Click" />&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click"/>&nbsp;&nbsp;&nbsp;
            </td>   
         </tr>
        <tr>
            <td style="width:25%;white-space:nowrap;" align="left">
              单据编号：<asp:TextBox ID="TextBoxPTCode" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
              生产制号：<asp:TextBox ID="TextBoxSCZH" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
               项目名称：<asp:TextBox ID="TextBoxProjname" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
              工程名称：<asp:TextBox ID="TextBoxEngname" runat="server"></asp:TextBox>
            </td>        
        </tr>
        
        <tr>
            <td style="width:25%;white-space:nowrap;" align="left">
               &nbsp;制&nbsp;单&nbsp;人：<asp:TextBox ID="TextBoxDoc" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
               制单日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
              &nbsp;技&nbsp;术&nbsp;员：<asp:TextBox ID="TextBoxVerifier" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
               提交日期：<asp:TextBox ID="TextBoxVerifyDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
              <td style="width:25%;white-space:nowrap;" align="left">
                &nbsp;审&nbsp;批&nbsp;人：<asp:TextBox runat="server" ID="TextBoxManager" ></asp:TextBox>
              </td>
            <td style="width:25%;white-space:nowrap;" align="left">
               审批日期：<asp:TextBox ID="TextBoxShPiDate" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        
    </table>
    
     </ContentTemplate>
    </asp:UpdatePanel>  
    </asp:Panel>   
        
   <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div>      
       <table id="superTable" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
        <asp:Repeater ID="Repeater1" runat="server"  >
            <HeaderTemplate>
            <tr align="center" class="tableTitle1" style="background-color: #e4ecf7">
                <td class="tdclass" ></td>
                <td class="tdclass">单据编号</td>
                <td class="tdclass">生产制号</td>
                <td class="tdclass">项目名称</td>
                <td class="tdclass">工程名称</td>
                <td class="tdclass">制单人</td>
                <td class="tdclass">制单日期</td>
                <td class="tdclass">技术员</td>
                <td class="tdclass">提交日期</td>
                <td class="tdclass">审批人</td>
                <td class="tdclass">审批日期</td>
                <td class="tdclass">状态</td>                             
                <td class="tdclass"></td>                
                                     
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr  style="background-color: #ffffff; border-width: 0px 1px 1px 0px; border-style: solid; border-color: #000000; text-align: center;" >
                <td class="tdclass"><%#Container.ItemIndex+1%></td>         
                <td class="tdclass"><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td class="tdclass"><%#Eval("SCZH")%></td>               
                <td class="tdclass"><%#Eval("PROJNAME")%></td>
                <td class="tdclass"><%#Eval("ENGNAME")%></td>
                <td class="tdclass"><%#Eval("DOC")%></td>
                <td class="tdclass"><%#Eval("DOCDATE")%></td>
                <td class="tdclass"><%#Eval("JSHY")%></td>
                <td class="tdclass"><%#Eval("JSHYDATE")%></td>
                <td class="tdclass"><%#Eval("MANAGER")%></td>
                <td class="tdclass"><%#Eval("MANAGERDATE")%></td>
                <td class="tdclass"><%#convertState((string)Eval("State"))%></td>   
                <td class="tdclass">
                     <asp:HyperLink ID="HyperLink1" CssClass="link"  Target="_blank" 
                         NavigateUrl='<%#"SM_Warehouse_ProjTemp.aspx?ID="+Eval("Code")+"&FLAG=OPEN" %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="state1" runat="server" ForeColor="Red" Text='<%# Eval("State").ToString()=="1"?"提交":Eval("State").ToString()=="2"?"审批":"<font color=Green>查看</font>" %>'></asp:Label>                                
                     </asp:HyperLink>
                </td> 
            </tr>
            </ItemTemplate>
        </asp:Repeater>
       
     </table>
     <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red" Visible="false" HorizontalAlign="Center" >没有相关记录!</asp:Panel>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />    
   
    </ContentTemplate>

    </asp:UpdatePanel>

     <div id="AlertDiv" class="AlertStyle">
     <img id="laoding" src="../Assets/images/ajaxloader.gif"  alt="downloading" />
     </div>
      <script type="text/javascript">
  
   var sDataTable=document.getElementById("superTable")
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = sDataTable.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                              
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#eeeeee";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

            </script>
             
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->                 

</asp:Content>

