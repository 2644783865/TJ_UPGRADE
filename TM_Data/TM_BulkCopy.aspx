<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMasterSession.Master" CodeBehind="TM_BulkCopy.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_BulkCopy" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<asp:Label ID="labprostru" runat="server" Text=""></asp:Label>
<JSR:JSRegister ID="JSRegister1" runat="server" /></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
     <script src="../JS/TM_ClearSelectedColumns.js" type="text/javascript" charset="GB2312"></script>
     <script src="../JS/TM_ColumnsContentReplace.js" type="text/javascript" charset="GB2312"></script>
     <script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script>   
     <script src="../JS/TM_ColumnsContentConditionReplace.js" type="text/javascript" charset="GB2312"></script> 
     <script language="javascript" type="text/javascript">

   function checkNums(obj)
   {
      var pattem=/^\d+$/;
      var txtnums=obj.value;
      if(!pattem.test(txtnums))
      {
         obj.value="1";
         alert("请输入正确的数量！！！");
      }
   }
   
   
   //勾选删除
   function checkDelete()
   {
     var tr=document.getElementById('<%=GridView1.ClientID %>').getElementsByTagName("tr");
     for(var i=1;i<tr.length;i++)
     {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {    
           document.getElementById('<%=txtDeleteNums.ClientID %>').value=i;
           break;
        }
        else
        {
           document.getElementById('<%=txtDeleteNums.ClientID %>').value="0";
        }
      }
    }
    //删除确认
    function DeleteConfirm()
    {
       var index=document.getElementById('<%=txtDeleteNums.ClientID %>').value;
       if(index=="0")
       {
          alert("请选择要删除的行！！！");
          return false;
       }
       else
       {
          var tt=confirm("确认删除吗？");
          if(tt==true)
          {
             return true;
          }
          else
          {
             return false;
          }
       }
    }
    function CloseWindow()
    {
       var ret=confirm("确认关闭窗口吗？")
       if(ret)
       {
          window.close();
       }
    }
    function getClientId()    {        var paraId1 = '<%= GridView1.ClientID %>';//注册控件1        var paraId2='<%= lblNumber.ClientID %>';        var paraId5='<%= txtBxishu.ClientID %>';        var paraId6='<%= txtXxishu.ClientID %>';        return {Id1:paraId1,Id2:paraId2,BXishu:paraId5,XXishu:paraId6};//生成访问器    }
    function ShowSetting()
    {
       var obj_showDiv=document.getElementById("clear_div");
       obj_showDiv.className = "show";
       ClearColumns(1);
       CallfunctionOfReplace(1);
       CallfunctionOfConReplace(1);
    }
    
    function CloseDiv()
    {
       var obj_showDiv=document.getElementById("clear_div");
       obj_showDiv.className = "hidden";
    }
</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="98%">
             <tr>
               <td style="width:15%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
             <td style="width:20%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:20%">工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>
            台数:<asp:TextBox ID="lblNumber" runat="server" Width="40px"></asp:TextBox>
            </td>
            <td align="right">
                <input id="btnClose" type="button" value="关 闭" onclick="return CloseWindow();" /></td>
             </tr>
           </table>
       </div>
     </div>
   </div>
    <div class="box-wrapper">
        <div class="box-outer">
      <table width="95%">
       <tr>
        <td style="width:10%;">&nbsp;&nbsp;&nbsp;<strong style="color:Red;">批量复制数据</strong></td>
        
        <td align="left">
            <input id="ckbTuhao" type="checkbox" />自动添加图号&nbsp;&nbsp;&nbsp;&nbsp;<input id="ckbXuhao" type="checkbox" checked="checked" />自动添加序号</td>
         <td align="right">
             <asp:Button ID="btnSave" runat="server" OnClientClick="return BomCheckMaoPi();" Visible="false" Text="保存" OnClick="btnSave_OnClick" />&nbsp;&nbsp;
             <asp:Button ID="btnDelete" runat="server" Visible="false" OnClientClick="DeleteConfirm();" Text="删除" OnClick="btnDelete_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <input id="txtDeleteNums" runat="server" value="0" type="text" readonly="readonly" style="display:none" />
         </td>
        <td align="left"><span style="color:Red;">
             <asp:Button ID="btnClearColum" OnClick="btnClearColum_OnClick" Visible="false" runat="server" Text="部件重量置0--取消使用" /></span></td> 
         <td align="left"><asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server" onclick="ShowSetting();"><asp:Image ID="Image4" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />设置...</asp:HyperLink></td>
         <td><span style="color:Red;">此页面录入数据效果与BOM录入界面相同</span></td>
       </tr>
      </table>
            <div id="clear_div" class="hidden" style="position:relative; background-color:#f3f3f3; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
              <table style="width:100%">
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
                  <td align="right" style="width:10%;"><strong>&nbsp;&nbsp;待清空列:</strong></td>
                  <td id="td_select" ></td>
                    <td align="right" style="width:10%;"><strong>待清空行:</strong></td>
                    <td>从<input id="txtStartIndex" type="text" onfocus="this.select();" onchange="AddControls_ClearSrtRow(this);" style="width:40px;" value="1" />行&nbsp;至&nbsp
                        <input id="txtEndIndex" style="width:40px;" onfocus="this.select();" onchange="AddControls_ClearEndRow(this);" type="text" value="1" />行
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;<input id="btnClearUserDefine" type="button"  value="清 空" onclick="return AddControls_Confirm();" /></td>
                    <td>
                        <input id="btnCloseClear" type="button" onclick="Hidden_clear_div();" value="关 闭" />
                    </td>    
                 <td  align="right"><strong>&nbsp;&nbsp;&nbsp;计划系数:</strong></td>
                 <td >(板)<input id="txtBxishu" runat="server" type="text" onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.1";}' style="width:30px"  value="1.1" />(型/圆钢)<input id="txtXxishu" runat="server" style="width:30px"  onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}' onfocus="this.select();"  type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width:30px" disabled="disabled"  type="text" value="1" /></td>
               </tr>              
               </table>
            </div>
            <br />
            <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal" RepeatColumns="17" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"  AutoPostBack="true" RepeatLayout="Table">
            </asp:CheckBoxList>
           <asp:Panel ID="NoDataPanel" runat="server" Visible="false"><div style="text-align:center; color:Red">没有记录!</div></asp:Panel>
           <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="return checkDelete()"
                                CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <input id="lblIndex" runat="server" readonly="readonly" style="border-style: none;
                                width: 30px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" ondblclick="ShowInput(this);" onkeydown="grControlFocusWithoutHiddden2(this);" onfocus="this.select();" onblur="BathAutoTuHao(this);" style="border-style: none; width: 150px" type="text"
                                value='<%#Eval("BM_TUHAO") %>' /><br />
                        </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" >
                        <ItemTemplate>
                            <asp:TextBox ID="marid" runat="server" onfocus="this.select();"  ToolTip="物料编码为空时,表明为部件，部件保存后无法修改物料编码！！！" BorderStyle="None"
                                onchange="autoMarCode(this)"  Text='<%# DataBinder.Eval(Container.DataItem, "BM_MARID")%>'
                                Width="120px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                                ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True">
                            </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="zongxu" runat="server"  ondblclick="ShowInput(this);"   onkeydown="grControlFocusWithoutHiddden2(this);" onblur="CopyEditverify(this);" onfocus="CopyEditAutoXuhao(this);this.select();" style="border-style: none;
                                width: 90px" type="text" value='<%#Eval("BM_ZONGXU") %>' /><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" onfocus="this.select();"  ondblclick="ShowInput(this);"  onkeydown="grControlFocusWithoutHiddden2(this);"   style="border-style: none; width: 150px" title="请在输入物料编码后再修改此项！！！"
                                type="text" value='<%#Eval("BM_CHANAME") %>' /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <input id="beizhu" runat="server" onfocus="this.select();"  ondblclick="ShowInput(this);"  onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 120px" type="text"
                                value='<%#Eval("BM_NOTE") %>' /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度">
                        <ItemTemplate>
                            <input id="cailiaocd" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  onchange="autoLW(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MALENGTH") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度">
                        <ItemTemplate>
                            <input id="cailiaokd" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" onchange="autoLW(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAWIDTH") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                        <ItemTemplate>
                            <input id="shuliang" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" onchange="autoNum(this)" style="border-style: none;width: 36px" type="text" value='<%#Eval("BM_SINGNUMBER") %>' />
                            |&nbsp;<input id="total_shuliang" runat="server" class="notbrk" tabindex="-1" title="不能修改"  readonly="readonly"  onfocus="this.blur();" value='<%#Eval("BM_NUMBER")%>' style="border-style: none;width: 36px" type="text" />
                            |&nbsp;<input id="p_shuliang" runat="server" tabindex="-1" value='<%#Eval("BM_PNUMBER") %>'  onfocus="this.select();" class="notbrk" onchange="autoP_Num(this)" title="提材料计划数量" style="border-style: none;width: 36px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="实际单重" >
                        <ItemTemplate>
                            <input id="dzh" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  onchange="autoRealUnitW(this);CheckBlukUnitWght_TuUnit(this);" style="border-style: none; width: 80px"
                                type="text" value='<%#Eval("BM_UNITWGHT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重" >
                        <ItemTemplate>
                            <input id="cailiaodzh" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 80px" onchange="autoMarUnitW(this)"
                                type="text" value='<%#Eval("BM_MAUNITWGHT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总重">
                        <ItemTemplate>
                            <input id="cailiaozongzhong" runat="server" onfocus="this.select();"  onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_MATOTALWGHT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="bgzmy" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  onchange="autoMyShpace(this)" title="输入单个数量面域即可！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MABGZMY") %>' />
                               |&nbsp;<input id="mpmy" runat="server" title="平方米:按【平米】采购计划，在此输入单个面域！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MPMY") %>' onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="";this.select();}}'  onkeydown='grControlFocusWithoutHiddden2(this.parentNode.getElementsByTagName("input")[0])'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上单重">
                        <ItemTemplate>
                            <input id="tudz" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 110px" type="text" onchange="autoPageW(this);"
                                value='<%#Eval("BM_TUUNITWGHT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上材质">
                        <ItemTemplate>
                            <input id="tucz" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUMAQLTY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上标准">
                        <ItemTemplate>
                            <input id="tubz" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUSTAD") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上问题">
                        <ItemTemplate>
                            <input id="tuwt" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUPROBLEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="caizhi" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAQUALITY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="guige" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 120px"
                                type="text" value='<%#Eval("BM_GUIGE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoname" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MANAME") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料规格" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoguige" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 120px" type="text" value='<%#Eval("BM_MAGUIGE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="理论重量" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="lilunzhl" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 56px" type="text" value='<%#Eval("BM_THRYWGHT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="zongzhong" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_TOTALWGHT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="cailiaozongchang" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_MATOTALLGTH") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="labunit" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 32px" type="text" value='<%#Eval("BM_MAUNIT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="biaozhun" runat="server" onfocus="this.select();"  readonly="readonly" style="border-style: none;
                                width: 150px" type="text" value='<%#Eval("BM_STANDARD") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="毛坯" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="xinzhuang" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);" style="border-style: none; width: 32px" type="text" onchange="ChangeofMarShapeBluck(this);"
                                value='<%#Eval("BM_MASHAPE") %>' />
                                 <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px; ">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                          </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="zhuangtai" runat="server" onfocus="findChoice(this,1,28);this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 32px" type="text"
                                value='<%#Eval("BM_MASTATE") %>' />
                                 <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                       </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工艺流程">
                        <ItemTemplate>
                            <input id="process" runat="server" onfocus="findChoice(this,3,29);this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_PROCESS") %>' />
                                  <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="英文名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <input id="en_name" runat="server" onfocus="this.select();" onkeydown="grControlFocusWithoutHiddden2(this);"  style="border-style: none; width: 70px" type="text"
                                value='<%#Eval("BM_ENGSHNAME") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="关键部件" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKeyComponents" runat="server" SelectedValue='<%#DataBinder.Eval(Container.DataItem, "BM_KEYCOMS") %>'>
                                <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="定尺">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFixedSize" runat="server" onchange="autoFixSize(this);" SelectedValue='<%#DataBinder.Eval(Container.DataItem, "BM_FIXEDSIZE") %>'>
                                <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                   
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header" TableHeight="500px"  TableWidth="99.9%" FixColumns="" />
            </yyc:SmartGridView>
    </div>
    </div> 
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 30%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:large;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>

