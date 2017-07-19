<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Pro_Stru_List_Tip.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Pro_Stru_List_Tip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <title>重要提示</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="text-align:center; font-size:large;"><br />关于BOM中各重量、长度的说明</div><br />
      <table  width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr><td align="center"></td>
        <td class="notbrk" align="center"><strong>主要用途</strong></td>
        <td class="notbrk" align="center"><strong>计算方式</strong></td>
        <td class="notbrk" align="center"><strong>能否编辑</strong></td>
        <td class="notbrk" align="center"><strong>备注</strong></td></tr>
        <tr></tr>
        <tr><td><strong>实际单重、实际总重、部件自身重量</strong></td>
        <td>制作明细中体现的重量</td>
        <td>按序号计算，剔除虚拟件重量</td>
        <td>对物料编辑有效，部件通过计算得到；明细提交后无法编辑</td>
        <td>部件自身重量是指剔除其下级物料后的重量，只有部件才能修改</td></tr>
        <tr><td><strong>材料单重、材料总重</strong></td>
        <td>提计划</td>
        <td>按序号计算，剔除虚拟件重量</td>
        <td>可编辑；计划提交后不可编辑</td>
        <td></td></tr>
        <tr><td><strong>图纸上单重、图纸上总重</strong></td>
        <td>某部件制作明细调整完成并计算后，可将实际单重、实际总重与图纸上单重、图纸上总重比较，便于找出可能存在的错误(一张图纸上的某部分调整到另一张图纸的情况除外)</td>
        <td>不计算</td>
        <td>可编辑</td>
        <td></td></tr>
        <tr><td><strong>单图实际单重、单图实际总重</strong></td>
        <td>单张图纸的实际重量</td>
        <td>按总序计算，剔除虚拟件重量</td>
        <td>不可编辑</td>
        <td></td></tr>
        <tr>
        <td><strong>材料长度、材料总长</strong></td>
        <td>提计划</td>
        <td>无</td>
        <td>可编辑；计划提交后不可编辑</td>
        <td>采购中需要按【米】采购的物料，按【材料总长】提计划</td>
        </tr>
      </table>
      <div><br /><strong>说明:</strong>在BOM录入及修改界面，所有输入的<span style="color:Red;">部件[实际单重]将不保存，输入的[实际单重]即为[图纸上单重]</span>，如果部件确实存在【部件自身重量】，请在【原始数据》查看】中修改。</div>
    </div>
    </form>
</body>
</html>
