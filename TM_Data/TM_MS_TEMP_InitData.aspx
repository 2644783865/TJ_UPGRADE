<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MS_TEMP_InitData.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_TEMP_InitData" %>
<%@ Register src="../Controls/UCPagingOfMS.ascx" tagname="UCPagingOfMS" tagprefix="uc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细      
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/MS_Adjust.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_ClearSelectedColumns.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script>   
    <script src="../JS/TM_ColumnsContentReplace.js" type="text/javascript" charset="GB2312"></script> 
    <script src="../JS/TM_ColumnsContentConditionReplace.js" type="text/javascript" charset="GB2312"></script> 
    <script language="javascript" type="text/javascript">
    var table;
    var tr;
    var obj;
    var objstr;
    function getClientId()
    {
        var paraId1 = '<%= GridView1.ClientID %>';
        var paraId2='<%= cbxXuhaoCopy.ClientID %>';
        var paraId3='<%= cbxAutoXuhao.ClientID %>';
        var paraId4='<%= cbxXuhaoSame.ClientID %>';
        var paraId5='<%= istid.ClientID %>';
        var paraId6='<%= txtid.ClientID %>';
        var paraId7='<%= cbxAutoTuhao.ClientID %>';
        var paraId8='<%= tsaid.ClientID %>';
        var paraId9='<%= cbxAutoMsXuhao.ClientID %>';
        var paraId10='<%= lblshortcut.ClientID %>';
        var paraId11='<%= pro_id.ClientID %>';
        var paraId12='<%= hdfTableName.ClientID %>';
        var paraId13='<%= hdfTaskid.ClientID %>';
        return {Grv:paraId1,cbxXuhaoCopy:paraId2,cbxAutoXuhao:paraId3,cbxXuhaoSame:paraId4,istid:paraId5,txtid:paraId6,cbxAutoTuhao:paraId7,tsaid:paraId8,cbxAutoMsXuhao:paraId9,lblshortcut:paraId10,lblproid:paraId11,tablename:paraId12,Taskid:paraId13};
    }
    //打开说明对话框
    function openToopTip()
    {
       window.open("TM_MS_ToolTip.aspx?MSNor=<%=PassedQueryString %>","pop","");
    }
    
    //添加组装部件
    function AddAssemblyPart()
    {
       var result=confirm("打开组装部件添加界面吗？");
       if(result==true)
       {
           var date=new Date();
           var time=date.getTime();
           var tt=window.showModalDialog("TM_MSAdjustInput.aspx?TaskID=<%=tsaid.Text %>&NoUse="+time,'',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
           if(tt=="Refesh")
           {
              window.location.reload();
           }
       }
    }
    function ShowSetting()
    {
       var obj_showDiv=document.getElementById("clear_div");
       obj_showDiv.className = "show";
       ClearColumns(0);
       CallfunctionOfReplace(2);
       CallfunctionOfConReplace(2);
    }
    //记录拆分
     function gotoMSAdjustOrg()
     {
       var result=confirm("提示：勾选您要拆分的记录以方便录入！！！\r\r确认跳转到拆分界面吗？");
       if(result==true)
       {
            table=document.getElementById(getClientId().Grv);
            tr=table.getElementsByTagName("tr");
            var ckb;
            var shuliang=0;
            var xuhao;
            var obj;
            for(var i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if(obj.checked)
                {
                   xuhao=tr[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
                   shuliang=tr[i].getElementsByTagName("td")[10].innerText;
                   break;
                }
            }
            if(shuliang==0||xuhao==null)
            {
                var aa=confirm("您没有勾选要拆分的项，建议勾选后继续以方便添加！！！\r\r确认继续吗？")
                if(aa==true)
                {
                        var frame = $get('IframeView');
      
                        frame.width=getWidth();
                        frame.height=getHeight(); 
                        var date=new Date();
                        frame.src = "TM_MSAdjustOrgInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao+"&nus="+date.getTime();
                        $find('ViewModalPopup').show();

                    ///window.open("TM_MSAdjustOrgInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao, "pop", " "); //保证只打开一个
                }
                else
                {
                   return false;
                }
            }
            else
            {
                var frame = $get('IframeView');

                frame.width=getWidth();
                frame.height=getHeight(); 
                frame.src = "TM_MSAdjustOrgInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao;
                $find('ViewModalPopup').show();

                ///////////window.open("TM_MSAdjustOrgInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao, "pop", " "); //保证只打开一个
            }
       }
     }
     
     
     
     /**/
         function ShowViewModal(ID) {
        var frame = $get('IframeView');
      
        frame.width=getWidth();
        frame.height=getHeight(); 
        frame.src = "";
        $find('ViewModalPopup').show();
    }
    
    function ViewOkayScript()
    {
      //在这里可以写相应的js事件
       RefreshDataGrid();
    }
    
    function RefreshDataGrid() 
    {
         document.getElementById("<%=btnSearch.ClientID%>").click();
    }
    
 //计算遮罩层的高
    function getHeight()
    {
        var winWidth
        if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
            winHeight = document.documentElement.scrollHeight;
        }
        else {
            winHeight = document.documentElement.clientHeight;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winHeight = winHeight;
        }
        else {
            winHeight = 600;
        }
        return winHeight;
    }
    
    //计算遮罩层的宽
    function getWidth(){
        var winWidth;
        if (document.documentElement.scrollWidth > document.documentElement.clientWidth) {
            winWidth = document.documentElement.scrollWidth;
        }
        else {
            winWidth = document.documentElement.clientWidth;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winWidth = winWidth;
        }
        else {
            winWidth = winWidth;
        }
        return winWidth;
    }
    
    
    function openLink(url) 
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Original_Data_Operate.aspx?NoUse="+time+"&register=" + url,'',"dialogHeight:700px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    
    
     //记录拆分
     function gotoMSAdjustSetOrg()
     {
       var result=confirm("提示：勾选您要拆分的记录以方便录入！！！\r\r确认跳转到拆分界面吗？");
      
       if(result==true)
       {
            
            table=document.getElementById(getClientId().Grv);
            tr=table.getElementsByTagName("tr");
            var ckb;
            var shuliang=0;
            var xuhao;
            var obj;
            for(var i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if(obj.checked)
                {
                 
                   xuhao=tr[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
                   shuliang=tr[i].getElementsByTagName("td")[10].innerText;
                   
                   break;
                }
            }
            if(shuliang==0||xuhao==null)
            {
                var aa=confirm("您没有勾选要拆分的项，建议勾选后继续以方便添加！！！\r\r确认继续吗？")
                if(aa==true)
                {
                        var frame = $get('IframeView');
      
                        frame.width=getWidth();
                        frame.height=getHeight(); 
                        var date=new Date();
                        frame.src = "TM_MSAdjustOrgSetInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao+"&nus="+date.getTime();
                        $find('ViewModalPopup').show();

                    ///window.open("TM_MSAdjustOrgSetInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao, "pop", " "); //保证只打开一个
                }
                else
                {
                   return false;
                }
            }
            else
            {
                var frame = $get('IframeView');
               
                frame.width=getWidth();
                frame.height=getHeight(); 
                frame.src = "TM_MSAdjustOrgSetInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao+"&nus="+date.getTime();
                $find('ViewModalPopup').show();

                ///////////window.open("TM_MSAdjustOrgSetInput.aspx?TaskID=<%=tsaid.Text %>&view_table=<%=view_table() %>&Xuhao="+xuhao, "pop", " "); //保证只打开一个
            }
       }
     }
    
    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:Button ID="btnSearch" runat="server" Text="Button" OnClick="btnSearch_OnClick" Style="display: none" />
    <asp:Button ID="ButtonEdit" runat="server" Text="查看" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderEdit" runat="server" CancelControlID="ButtonViewCancel"
        OkControlID="ButtonViewDone" TargetControlID="ButtonEdit" PopupControlID="DivEViewWindow"
        OnOkScript="ViewOkayScript();" BehaviorID="ViewModalPopup">
    </cc1:ModalPopupExtender>
    <div style="display: none">
        <input id="ButtonViewDone" value="Done" type="button" />
        <input id="ButtonViewCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEViewWindow" style="display: none; background-color: #00FFFF;">
        <iframe id="IframeView" frameborder="0" scrolling="no"></iframe>
    </div>
<div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="100%">
        <tr>
            <td>生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
                <asp:HiddenField ID="hdfTableName" runat="server" />
                <asp:HiddenField ID="hdfTaskid" runat="server" />
            </td>
            <td >项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" runat="server" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>台数:<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label></td>
            <td>
             &nbsp;显示级数:<asp:DropDownList ID="ddlOrgJishu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Event_Query">
           <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
             <asp:ListItem Text="1级" Value="0"></asp:ListItem>
             <asp:ListItem Text="2级" Value="1"></asp:ListItem>
             <asp:ListItem Text="3级" Value="2"></asp:ListItem>
             <asp:ListItem Text="4级" Value="3"></asp:ListItem>
             <asp:ListItem Text="5级" Value="4"></asp:ListItem>
             <asp:ListItem Text="6级" Value="5"></asp:ListItem>
             <asp:ListItem Text="7级" Value="6"></asp:ListItem>
             <asp:ListItem Text="8级" Value="7"></asp:ListItem>
           </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlShowType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Event_Query">
            <asp:ListItem Text="按总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
            <asp:ListItem Text="按序号" Value="BM_XUHAO"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center">
                        库：<asp:DropDownList ID="ddlKU" runat="server" OnSelectedIndexChanged="Event_Query" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:HyperLink ID="HyperLink3" ToolTip="如果您要将原始数据中的一条或多条记录组装在一起,请点击这里，添加组装父序！！！" runat="server" CssClass="hand" onclick="return AddAssemblyPart();"><asp:Image ID="Image4" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />添加组装部件</asp:HyperLink>
            </td>
            <td align="center">
                <asp:HyperLink ID="HyperLink4" onclick="return gotoMSAdjustOrg();" ToolTip="总序要拆分时,请点击这里,可输入总序相同的记录！！！需要注意的是：(1)拆分部件:如果要将某一记录拆分成2条，则再输入一条即可，依次类推；(2)、拆分物料:如将某一记录【扁钢】拆分成2条,则需要按您的拆分要求再输入2条记录,这2条记录是用于调整制作明细的，不提计划，提计划用原始记录。" runat="server" CssClass="hand"><asp:Image ID="Image5" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />单条拆分</asp:HyperLink>
            </td>
            <td align="center">
                <asp:HyperLink ID="HyperLink2" ToolTip="将成套设备均分成多条！！！" runat="server" onclick="return gotoMSAdjustSetOrg();" CssClass="hand"><asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />成套拆分</asp:HyperLink>
            </td>
            <td align="center">
                <asp:HyperLink ID="hylKUCheck" ForeColor="Black" Target="_blank" ToolTip="核对含“库”重量！！！" runat="server"><asp:Image ID="Image7" runat="server" ImageUrl="~/Assets/icons/gadgets.gif" />库重量核对</asp:HyperLink>
            </td>
            <td align="center">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" Visible="false" onclick="openToopTip();"><asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/icons/gadgets.gif" />查看</asp:HyperLink>
            </td>

         </tr>
        </table>
       </div>
      </div>
    </div>
       <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
        <tr>
        <td style="width:80px"><strong>查询条件>></strong></td>
        <td align="left">
          <table width="100%">
           <tr>
              <td>部件名称:<asp:DropDownList ID="ddlParts" runat="server" AutoPostBack="True" onselectedindexchanged="Event_Query">
                </asp:DropDownList></td>
                <td>体现:<asp:DropDownList ID="ddlinMS" runat="server" AutoPostBack="True" onselectedindexchanged="Event_Query">
                  <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                  <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                  <asp:ListItem Text="否" Value="N"></asp:ListItem>
                </asp:DropDownList></td>
                <td align="right"><asp:DropDownList ID="ddlXH_ZX"  runat="server" >
                    <asp:ListItem Text="-查询方式-" Value="-请选择-"></asp:ListItem>
                    <asp:ListItem Text="序号查询" Value="BM_XUHAO"></asp:ListItem>
                    <asp:ListItem Text="总序查询" Value="BM_ZONGXU"></asp:ListItem>
                    <asp:ListItem Text="明细序号" Value="BM_MSXUHAO"></asp:ListItem>
                    <asp:ListItem Text="图号(标识号)" Value="BM_TUHAO"></asp:ListItem>
                    <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>
                  </asp:DropDownList></td>
                  <td align="left"><asp:TextBox ID="txtXH_ZX" runat="server"></asp:TextBox>
                  &nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="Event_Query" />
                  </td>
           </tr>
          </table>
                </td>
            <td align="center" rowspan="2">
                <asp:Button ID="btnDataSave" runat="server" Text="保 存" OnClientClick="return CheckMS_IN_KU();" OnClick="btnDataSave_Onclick" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnsave" runat="server" Text="重量计算" OnClientClick="return confirm('确认进行重量计算吗？\r\r提示:\r\r处理数据量大,可能导致计算速度慢！！！\r\r您只需要在生成制作明细前计算一次即可！！！')" ToolTip="根据【序号】计算单重和总重！" onclick="btnSave_onClick"/>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMS" runat="server" OnClientClick="return confirm('确认进入制作明细生成界面？\r\r提示:\r\r制作明细未生成！！！\r\r请确认在制作明细生成前已进行【重量计算】！！！')" Text="明细预览" OnClick="btnMS_OnClick" />

            </td>
        </tr>
        <tr>
               <td style="width:80px"><strong>操作方式>></strong></td>
               <td align="left">
               <asp:HyperLink ID="hylMore" CssClass="hand" runat="server"><asp:Image ID="Image3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />快捷操作</asp:HyperLink>
                   <input id="lblshortcut" runat="server" readonly="readonly" style="color:Red; background-color:Transparent; border-style:none;" value="(当前:无)" type="text" />
               <cc1:HoverMenuExtender ID="HoverMenuExtender1" TargetControlID="hylMore" OffsetX="50" OffsetY="20" PopupControlID="Panel1" runat="server">
                            </cc1:HoverMenuExtender>
                            <asp:Panel ID="Panel1" runat="server" style="display:none;">
                             <table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
                                <tr><td>
                                    序号复制数据:<input id="cbxXuhaoCopy" onclick="CheckSelected(this);" runat="server" type="checkbox" title="勾选时按【序号】插入数据，未勾选时按【总序】插入数据" /></td></tr>
                 <tr><td>
               自动添加序号:<input id="cbxAutoXuhao" onclick="CheckSelected(this);" runat="server" type="checkbox" title="勾选后按照上一行序号直接带出下一行序号" />
               </td></tr>
               <tr><td>
               序号总序一致:<input id="cbxXuhaoSame" onclick="CheckSelected(this);" runat="server" type="checkbox" title="勾选后,点击要修改序号所在单元格即可保证序号与总序一致" />
               </td></tr>  
               <tr><td>           
               自动添加图号:<input id="cbxAutoTuhao" onclick="CheckSelected(this);" runat="server" type="checkbox" title="勾选后按上一行自动带出下一行图号" />
               </td></tr>
               <tr><td>
               自动明细序号:<input id="cbxAutoMsXuhao" onclick="CheckSelected(this);" runat="server" type="checkbox" title="勾选后按照上一行明细序号直接带出下一行明细序号" />
               </td></tr>
               </table>
               </asp:Panel>
               
               &nbsp;&nbsp;排序方式：
            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_OnSelectedIndexChanged">
              <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem>
              <asp:ListItem Text="综合" Value="BM_ORDERINDEX"></asp:ListItem>
              <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <input id="btnClear" type="button" value="更多操作" onclick="ShowSetting();" />&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btninsert" runat="server" Text="插 入" ToolTip="插入所选行下方/保存上次操作" OnClientClick="return insert()" onclick="btninsert_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btndelete" runat="server" Text="删 除" ToolTip="只在界面上删除！" OnClientClick="return check()" onclick="btndelete_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnKuBiaoshi" type="button" value="库标识号" onclick="AddKuBiaoshi();" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnMSXuhao" type="button" value="明细序号" onclick="AddMsXuhao();" />
            </td>
        
        </tr>
        </table>
        <div id="clear_div" class="hidden" style="position:relative; background-color:#f3f3f3; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                <table width="100%">
                <tr>
                  <td align="right" style="width:10%;"><strong>&nbsp;&nbsp;待替换列:</strong></td>
                  <td id="td_select_replace" ></td>
                  <td align="right" style="width:10%;"><strong>待替换行:</strong></td>
                  <td>从<input id="txtReplaceStrIndex" type="text" onfocus="this.select();" onchange="AddControls_ReplaceSrtRow(this);" style="width:40px;" value="1" />行&nbsp;至&nbsp
                        <input id="txtReplaceEndIndex" style="width:40px;" onfocus="this.select();" onchange="AddControls_ReplaceEndRow(this);" type="text" value="1" />行
                  </td>
                  <td align="right" style="width:10%;"><strong>替换内容:</strong></td> 
                  <td><input id="txtOldContent" type="text" onfocus="this.select();" onmouseover="this.title=this.value;"  ondblclick="ShowInput(this);"  style="width:100px;" value="" />替换为
                      <input id="txtNewContent" style="width:100px;" onfocus="this.select();" onmouseover="this.title=this.value;" ondblclick="ShowInput(this);"   type="text" value="" />
                  <br /></td>
                    <td>&nbsp;&nbsp;&nbsp;<input id="btnReplace" type="button"  value="替 换" onclick="return AddControls_ReplaceConfirm();" /></td>
                    <td>
                        <input id="btnCloseReplace" type="button" onclick="Hidden_clear_div();" value="关 闭" />
                    </td>   
                  </tr>
               <tr><td colspan="8"><hr style="height:0.2px;" /></td></tr>
               <tr>
                  <td align="right" style="width:10%;"><strong>&nbsp;&nbsp;条件替换:</strong></td>
                  <td id="td_select_conditionreplace" ></td>
                  <td align="right" style="width:10%;"><strong>待替换行:</strong></td>
                  <td>从<input id="txtConditionReplaceStrIndex" type="text" onfocus="this.select();" onchange="AddControls_ConReplaceSrtRow(this);" style="width:40px;" value="1" />行&nbsp;至&nbsp
                        <input id="txtConditionReplaceEndIndex" style="width:40px;" onfocus="this.select();" onchange="AddControls_ConReplaceEndRow(this);" type="text" value="1" />行
                  </td>
                  <td align="right" style="width:10%;"><strong>替换条件:</strong></td> 
                  <td><input id="txtStartContent" type="text" onfocus="this.select();" onmouseover="this.title=this.value;"  ondblclick="ShowInput(this);"  style="width:60px;" value="" />&nbsp;
                      至&nbsp;<input id="txtEndContent" style="width:60px;" onfocus="this.select();" onmouseover="this.title=this.value;" ondblclick="ShowInput(this);"   type="text" value="" />之间&nbsp;&nbsp;
                      替换成<input id="txtConditionContent" style="width:60px;" type="text" />
                  <br /></td>
                    <td>&nbsp;&nbsp;&nbsp;<input id="btnConditionReplace" type="button"  value="替 换" onclick="return AddControls_ConReplaceConfirm();" /></td>
                    <td>
                        <input id="btnCloseConditionReplace" type="button" onclick="Hidden_clear_div();" value="关 闭" />
                    </td>   
               </tr>
                <tr><td colspan="8"><hr style="height:0.2px;" /></td></tr>
                <tr>
                  <td align="right" style="width:10%;"><strong>待清空列:</strong></td>
                  <td id="td_select" ></td>
                    <td align="right" style="width:10%;"><strong>待清空行:</strong></td>
                    <td>从<input id="txtStartIndex" type="text" onchange="AddControls_ClearSrtRow(this);" style="width:40px;" value="1" />行&nbsp;至&nbsp
                        <input id="txtEndIndex" style="width:40px;" onchange="AddControls_ClearEndRow(this);" type="text" value="1" />行
                    </td>
                    <td><input id="btnClearUserDefine" type="button"  value="清 空" onclick="return AddControls_Confirm();" /></td>
                    <td>
                        <input id="btnCloseClear" type="button" onclick="Hidden_clear_div();" value="关 闭" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>

                </table>
               </div>
 <div class="box-wrapper">
     <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center"><span style="font-size:medium"><br />没有记录!</span></asp:Panel>
       <yyc:SmartGridView ID="GridView1"  runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" onclick="SelectOne(this);" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="明细序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input ID="MsIndex" runat="server" onkeydown="grControlFocus(this)" onfocus="AutoMsXuhao(this);this.select();" style="border-style:none; width:60px;" type="text" value='<%#Eval("BM_MSXUHAO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input ID="Index" runat="server" title="不能为空!!!" onmouseover="this.title=this.value;" onkeydown="grControlFocus(this)" onfocus="DefaultXuHao(this);this.select();" onclick="XuHaoSame(this);" onchange="copyXuhao(this)" style="border-style:none;width:90px;" CssClass="notbrk" type="text" value='<%#Eval("BM_XUHAO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图号(标识号)" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input ID="tuhao" runat="server"  ondblclick="ShowInput(this);"  onmouseover="this.title=this.value;"  onkeydown="grControlFocus(this)" style="border-style:none;" onfocus="this.select();"  onblur="msAutoTuHao(this);"
                                    type="text" value='<%#Eval("BM_TUHAO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input ID="zongxu" runat="server"    onmouseover="this.title=this.value;" onkeydown="grControlFocus(this)" style="border-style:none;width:90px;" 
                             onchange="copyZongxu(this)" onfocus="this.select();" title="请勿改变总序编号！" type="text" value='<%#Eval("BM_ZONGXU") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                       <ItemTemplate>
                           <asp:TextBox ID="txtName" CssClass="notbrk"  ondblclick="ShowInput(this);"  onmouseover="this.title=this.value;" onfocus="this.select();" style="border-style:none;" onkeydown="grControlFocus(this)" runat="server" Text='<%#Eval("BM_CHANAME") %>'></asp:TextBox>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center"  HeaderText="体现" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                          <ItemTemplate>
                              <asp:DropDownList ID="ddlISMANU"  runat="server" SelectedValue='<%#Eval("BM_ISMANU") %>'>
                                <asp:ListItem Text="N" Value="N" ></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                              </asp:DropDownList>
                          </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                           <ItemTemplate>
                             <input ID="ku" runat="server"  onfocus="findChoice(this,2,8);this.select();" onchange="ChangeGYLC(this);"  onmouseover="this.title=this.value;" onkeydown="grControlFocus(this)" style="border-style:none;width:20px;" type="text" value='<%#Eval("BM_KU") %>' />
                              <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                           </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                           <ItemTemplate>
                             <input ID="guige" runat="server"  ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)" onfocus="this.select();" onmouseover="this.title=this.value;" style="border-style:none;width:100px;" type="text" value='<%#Eval("BM_GUIGE") %>' />
                           </ItemTemplate>
                   </asp:TemplateField>
                    <asp:BoundField DataField="NUMBER"  HeaderText="数量"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:TemplateField HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                           <ItemTemplate>
                             <input ID="process" runat="server" onfocus="findChoice(this,3,11);this.select();" onchange="ChangeGYLC(this);"  onmouseover="this.title=this.value;" onkeydown="grControlFocus(this)" style="border-style:none;width:80px;" CssClass="notbrk" type="text" value='<%#Eval("BM_PROCESS") %>' />
                              <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                           </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                           <ItemTemplate>
                             <input ID="beizhu" runat="server"  onclick="ShowInput(this);"  onkeydown="grControlFocus(this)" onfocus="this.select();" onmouseover="this.title=this.value;" style="border-style:none;" type="text" CssClass="notbrk" value='<%#Eval("BM_NOTE") %>' />
                           </ItemTemplate>
                   </asp:TemplateField>                   
                    <asp:BoundField DataField="BM_MAQUALITY"  HeaderText="材质" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                    
                    <asp:BoundField DataField="BM_UNITWGHT"  DataFormatString="{0:N2}" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />

                    <asp:BoundField DataField="BM_TOTALWGHT"  DataFormatString="{0:N2}" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />

                        <asp:BoundField DataField="BM_MASHAPE"  HeaderText="毛坯" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="BM_MASTATE"  HeaderText="状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />

                        <asp:BoundField DataField="BM_STANDARD"  HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="BM_KEYCOMS"  HeaderText="关键部件" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />

                        <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0" HeaderStyle-Wrap="false">
                           <ItemTemplate>
                              <input id="hdfID" type="hidden" value='<%#Eval("BM_ID") %>' runat="server" style="width:0" />
                              <input id="hdfOSSTATE" runat="server" type="hidden" value='<%#Eval("BM_OSSTATE") %>' />
                           </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" CssClass="freezing"   ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="600px" TableWidth="100%" FixColumns="0,1" />
            </yyc:SmartGridView>
            
                <uc2:UCPagingOfMS ID="UCPaging2" runat="server" />
            <input id="hdbtn" runat="server" type="button"  OnServerClick="hdbtn_Click" style="display:none" />
            <input id="btnxuhaoQuery" runat="server" type="button"  OnServerClick="hdbtnxuhao_Click" style="display:none" />
            <input id="zongxu" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="index" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="bgid" type="text" value="0" runat="server" readonly="readonly" style="display: none" />
            <input id="txtid" type="text" value="0" runat="server" readonly="readonly" style="display: none" />
            <input id="istid" type="text" value="0" runat="server" readonly="readonly" style="display: none" />
</div>

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 50%; right:45%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
</asp:Content>
